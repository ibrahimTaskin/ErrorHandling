using API.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Response'ları kontrol edeceğimiz yapı
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound(); // Kendi null gelirse notFound dön

            if (result.IsSuccess && result.Value != null) // succes ve dönen value null değilse değeri göster.
                return Ok(result.Value);

            if (result.IsSuccess && result.Value == null) // succes ama dönen value null ise notFound
                return NotFound();

            return BadRequest(result.Error); // hiç biri değilse BadRequest
        }
    }
}
