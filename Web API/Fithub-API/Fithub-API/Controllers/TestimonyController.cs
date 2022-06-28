using Fithub_BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Fithub_API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TestimonyController : ControllerBase
  {
        private readonly IQueryTestimony _queryTestimony;

        public TestimonyController(IQueryTestimony queryTestimony)
        {
            _queryTestimony = queryTestimony?? throw new ArgumentNullException(nameof(queryTestimony));
        }

        [HttpGet]
        [Route("getUserTestimony/{UserID}")]
        [Authorize(Roles = "VIEWER")]
        public IActionResult GetUserTestimony([FromRoute]int UserID)
        {
            try
            {
                var result = _queryTestimony.QueryTestimonyByUser(UserID);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + exception.Message);
                throw;
            }
            
        }
  }
}
