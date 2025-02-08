
using System.Text.RegularExpressions;

namespace GridCraftTableGenDotNetWebApi.GridGeneration
{
    public partial class GridGenerationService
    {
        public string[][] GenerateGrid(GridInput input)
        {
            var grid = new string[input.NumberOfRows + 1][];
            for (var i = 0; i <= input.NumberOfRows; i++)
            {
                grid[i] = new string[input.Columns.Length];

                if (i == 0)
                {
                    for (var j = 0; j < input.Columns.Length; j++)
                    {
                        grid[i][j] = input.Columns[j].Name;
                    }

                    continue;
                }

                for (var j = 0; j < input.Columns.Length; j++)
                {
                    grid[i][j] = EvaluateExpression(
                        input.Columns[j].Expression,
                        i - 1 // Index starts from 0
                    );
                }
            }

            return grid;
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
                var v1 = match.Groups[1].Value;
                var v2 = v1.Replace("i", rowIndex.ToString());
                var v3 = new NCalc.Expression(v2).Evaluate();
                return v3.ToString() ?? throw new InvalidOperationException($"Invalid expression: {expression}");
            });

            return result;
        }

        [GeneratedRegex(@"\{(.*?)\}")]
        private static partial Regex MathRegex();
    }
}