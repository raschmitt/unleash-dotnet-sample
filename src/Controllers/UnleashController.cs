using Microsoft.AspNetCore.Mvc;
using Unleash;
using unleash_dotnet_sample.Models;

namespace unleash_dotnet_sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnleashController : ControllerBase
    {
        private readonly IUnleash _unleash;

        public UnleashController(IUnleash unleash)
        {
            _unleash = unleash;
        }
        
        [HttpGet("{featureToggle}")]
        public IActionResult Get([FromRoute] string featureToggle)
        {
            var response = new Response(featureToggle, _unleash.IsEnabled(featureToggle));
            
            return Ok(response);
        }

    }
}