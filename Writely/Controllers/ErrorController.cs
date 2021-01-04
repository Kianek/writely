using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Writely.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment env)
        {
            if (env.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in a development environment");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return Problem(context.Error.StackTrace, context.Error.Message);
        }
        
        [Route("error")]
        public IActionResult Error() => Problem();
    }
}