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
                    new GridColumnInput { Name = "Demand", Expression = "{Rand(4000, 6000, 1)}" },
                    new GridColumnInput { Name = "Marketing Spend", Expression = "{Rand(8000, 20000, 1000)}" },
                    new GridColumnInput { Name = "GDP Growth", Expression = "{Rand(2, 4, 0.1)}%" },
                    new GridColumnInput { Name = "Holiday Season", Expression = "{Rand(0, 1, 1)}" }
                ]
            };

            // Act
            service.GenerateGrid(input);
        }
    }
}