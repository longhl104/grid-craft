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
        public async Task<ActionResult<string>> GenerateGrid(GridInput input)
        {
            return Ok(await gridGenerationService.GenerateGrid(input));
        }
    }
}
