using GridCraftTableGenDotNetWebApi.GridGeneration;

namespace GridCraftTableGenDotNetWebApi.Tests.GridGeneration
{
    public class GridGenerationServiceTest
    {
        [Fact]
        public void GenerateGrid_ShouldReturnCorrectGrid_WhenValidInput()
        {
            // Arrange
            var service = new GridGenerationService();
            var input = new GridInput
            {
                NumberOfRows = 5,
                Columns =
                [
                    new GridColumnInput { Name = "Date", Expression = "{2020 + Truncate(i / 4) }-Q{i % 4 + 1}" },
                    new GridColumnInput { Name = "Demand", Expression = "5000" }
                ]
            };

            // Act
            var result = service.GenerateGrid(input);
            foreach (var row in result)
            {
                Console.WriteLine(string.Join(", ", row));
            }
        }
    }
}