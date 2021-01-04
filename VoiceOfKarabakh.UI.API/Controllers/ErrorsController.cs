using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceOfKarabakh.UI.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public Exception Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = context.Error;

            int statusCode = StatusCodes.Status500InternalServerError;

            if (error is ArgumentNullException)
                statusCode = StatusCodes.Status404NotFound;

            HttpContext.Response.StatusCode = statusCode;

            return error;
        }
    }
}
