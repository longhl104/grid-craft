
using System.Text;
using System.Text.RegularExpressions;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace GridCraftTableGenDotNetWebApi.GridGeneration
{
    public partial class GridGenerationService(IAmazonS3 amazonS3)
    {
        private const string BucketName = "development-gridcrafttablegenerationstack-bucket";

        public async Task<string> GenerateGrid(GridInput input)
        {
            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
            for (var i = 0; i <= input.NumberOfRows; i++)
            {
                var values = new string[input.Columns.Length];

                if (i == 0)
                {
                    for (var j = 0; j < input.Columns.Length; j++)
                    {
                        values[j] = input.Columns[j].Name;
                    }
                }
                else
                {
                    for (var j = 0; j < input.Columns.Length; j++)
                    {
                        values[j] = EvaluateExpression(
                            input.Columns[j].Expression,
                            i - 1 // Index starts from 0
                        );
                    }
                }

                streamWriter.WriteLine(string.Join(",", values));
            }

            // Ensure data is flushed to stream
            streamWriter.Flush();

            // Convert stream to string (if needed)
            memoryStream.Position = 0; // Reset position to start of stream before uploading

            // Upload to S3
            var transferUtility = new TransferUtility(amazonS3);
            var key = $"{Guid.NewGuid()}.csv";
            await transferUtility.UploadAsync(memoryStream, BucketName, key);

            // Generate a pre-signed URL
            var url = amazonS3.GetPreSignedURL(new GetPreSignedUrlRequest
            {
                BucketName = BucketName,
                Key = key,
                Expires = DateTime.Now.AddHours(1)
            });

            return url;
        }


        /// <summary>
        /// Evaluates a mathematical expression string, replacing occurrences of 'i' with the specified row index,
        /// and returns the result as a string.
        /// </summary>
        /// <param name="expression">The mathematical expression to evaluate.</param>
        /// <param name="rowIndex">The row index to replace 'i' with in the expression. The row index starts from 0.</param>
        /// <returns>The result of the evaluated expression as a string.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the expression is invalid.</exception>
        private static string EvaluateExpression(string expression, int rowIndex)
        {
            string result = MathRegex().Replace(expression, match =>
            {
                var expressionWithIndex = match.Groups[1].Value;
                var replacedExpression = expressionWithIndex.Replace("i", rowIndex.ToString());
                var ncalcExpression = new NCalc.Expression(replacedExpression);
                ncalcExpression.EvaluateFunction += NCalcExtensionFunctions;
                var evaluatedResult = ncalcExpression.Evaluate();
                return evaluatedResult.ToString() ?? throw new InvalidOperationException($"Invalid expression: {expression}");
            });

            return result;
        }

        private static void NCalcExtensionFunctions(string name, NCalc.FunctionArgs functionArgs)
        {
            if (string.Equals(name, "Rand", System.StringComparison.Ordinal))
            {
                var min = Convert.ToDecimal(functionArgs.Parameters[0].Evaluate());
                var max = Convert.ToDecimal(functionArgs.Parameters[1].Evaluate());
                var difference = functionArgs.Parameters.Length > 2
                    ? Convert.ToDecimal(functionArgs.Parameters[2].Evaluate())
                    : (decimal?)null;

                functionArgs.Result = GetRandomDecimal(min, max, difference);
            }
        }

        private static decimal GetRandomDecimal(decimal min, decimal max, decimal? difference = null)
        {
            Random random = new();

            if (difference.HasValue)
            {
                if (difference.Value <= 0)
                    throw new ArgumentException("Difference must be greater than zero.");

                int steps = (int)((max - min) / difference.Value);
                int randomStep = random.Next(0, steps + 1); // Inclusive
                return min + (randomStep * difference.Value);
            }
            else
            {
                // Generate a truly random decimal in the range
                double range = (double)(max - min);
                double randomDouble = random.NextDouble() * range;
                return min + (decimal)randomDouble;
            }
        }

        #region Regex Patterns

        [GeneratedRegex(@"\{(.*?)\}")]
        private static partial Regex MathRegex();

        #endregion
    }
}