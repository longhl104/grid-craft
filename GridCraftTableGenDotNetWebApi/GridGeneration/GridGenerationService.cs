
namespace GridCraftTableGenDotNetWebApi.GridGeneration
{
    public class GridGenerationService
    {
        public string[][] GenerateGrid(GridInput input)
        {
            var grid = new string[input.NumberOfRows][];
            for (var i = 0; i < input.NumberOfRows; i++)
            {
                grid[i] = new string[input.Columns.Length];
                for (var j = 0; j < input.Columns.Length; j++)
                {
                    grid[i][j] = input.Columns[j].Expression;
                }
            }

            return grid;
        }
    }
}