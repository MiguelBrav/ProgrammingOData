using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models;

namespace ProgrammingOData.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrlanguagesController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new List<PrLanguage>());
        }
    }
}
