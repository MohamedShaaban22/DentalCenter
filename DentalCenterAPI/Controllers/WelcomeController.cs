using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers
{
    [ApiController]
    public class WelcomeController : ControllerBase
    {
        [HttpGet("")]
        public ActionResult Welcome()
        {
            string version = GetType().Assembly.GetName().Version.ToString();
            return Ok(version);
        }
    }
}