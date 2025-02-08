using Microsoft.AspNetCore.Mvc;

namespace GridCraftTableGenDotNetWebApi.GridGeneration
{
    [Route("api/[controller]")]
    [ApiController]
    public class GridGenerationController : ControllerBase
    {
        /// <summary>
        /// Generates a grid based on the input.
        /// </summary>
        [HttpPost]
        public ActionResult<string[][]> GenerateGrid(GridInput input)
        {
            return Ok();
        }
    }
}
