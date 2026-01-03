using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        // Conceito de health-check para verificar se API está disponível para uso.
        [HttpGet("")]
        public IActionResult Get() => Ok();
    }
}
 