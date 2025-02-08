namespace GridCraftTableGenDotNetWebApi.GridGeneration
{
    public class GridInput
    {
        public required int NumberOfRows { get; set; }
        public required GridColumnInput[] Columns { get; set; }
    }
}