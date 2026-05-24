using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GestaoFinanceira.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RenderNaoMorra : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> NaoMorraRender()
        {
            return Ok("Não morra render!!");
        } 
    }
}
