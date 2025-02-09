using Amazon.S3;
using GridCraftTableGenDotNetWebApi.GridGeneration;

namespace GridCraftTableGenDotNetWebApi.Tests.GridGeneration
{
    public class GridGenerationServiceTest
    {
        private readonly IAmazonS3 amazonS3;

        public GridGenerationServiceTest()
        {
            amazonS3 = new AmazonS3Client();
        }

        [Fact]
        public async Task GenerateGrid_ShouldReturnCorrectGrid_WhenValidInput()
        {
            // Arrange
            var service = new GridGenerationService(amazonS3);
            var input = new GridInput
            {
                NumberOfRows = 5,
                Columns =
                [
                    new GridColumnInput { Name = "Date", Expression = "{2020 + Truncate(i / 4) }-Q{i % 4 + 1}" },
                    new GridColumnInput { Name = "Demand", Expression = "{Rand(4000, 6000, 1)}" },
                    new GridColumnInput { Name = "Marketing Spend", Expression = "{Rand(8000, 20000, 1000)}" },
                    new GridColumnInput { Name = "GDP Growth", Expression = "{Rand(2, 4, 0.1)}%" },
                    new GridColumnInput { Name = "Holiday Season", Expression = "{Rand(0, 1, 1)}" }
                ]
            };

            // Act
            var url = await service.GenerateGrid(input);
            Console.WriteLine($"Generated grid at: {url}");
        }
    }
}

