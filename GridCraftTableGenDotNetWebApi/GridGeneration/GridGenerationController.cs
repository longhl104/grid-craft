using Microsoft.AspNetCore.Mvc;

namespace GridCraftTableGenDotNetWebApi.GridGeneration
{
    [Route("api/[controller]")]
    [ApiController]
    public class GridGenerationController(GridGenerationService gridGenerationService) : ControllerBase
    {
        /// <summary>
        /// Generates a grid based on the input.
        /// </summary>
        [HttpPost]
        public ActionResult<string[][]> GenerateGrid(GridInput input)
        {
            return Ok(gridGenerationService.GenerateGrid(input));
        }
    }
}
