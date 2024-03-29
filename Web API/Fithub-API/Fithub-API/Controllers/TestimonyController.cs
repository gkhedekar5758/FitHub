using Fithub_API.Helper;
using Fithub_BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Fithub_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestimonyController : ControllerBase
    {
        private readonly IQueryTestimony _queryTestimony;
        private readonly IFithubConfigHelper _fithubConfigHelper;

        public TestimonyController(IQueryTestimony queryTestimony, IFithubConfigHelper fithubConfigHelper)
        {
            _queryTestimony = queryTestimony ?? throw new ArgumentNullException(nameof(queryTestimony));
            this._fithubConfigHelper = fithubConfigHelper;
        }

        [HttpGet]
        [Route("getUserTestimony/{UserID}")]
        [Authorize(Roles = "VIEWER")]
        public IActionResult GetUserTestimony([FromRoute] int UserID)
        {
            try
            {
                var result = _queryTestimony.QueryTestimonyByUser(_fithubConfigHelper.FithubConnectionString, UserID);
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
