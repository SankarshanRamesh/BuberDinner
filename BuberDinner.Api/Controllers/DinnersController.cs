using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class DinnersController : ApiController
    {
        [HttpGet]
        public IActionResult GetDinners()
        {
            return Ok(Array.Empty<string>());
        }
    }
}
