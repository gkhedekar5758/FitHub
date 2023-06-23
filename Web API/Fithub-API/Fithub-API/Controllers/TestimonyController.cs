using Fithub_API.Helper;
using Fithub_BL.Interfaces;
using Fithub_Data.DTO;
using Fithub_Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Fithub_API.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/user/{UserID}/[controller]")] //test
    public class TestimonyController : ControllerBase
    {
        private readonly IQueryTestimony _queryTestimony;
        private readonly IUpdateTestimony _updateTestimony;
        private readonly IFithubConfigHelper _fithubConfigHelper;

        public TestimonyController(IQueryTestimony queryTestimony, IUpdateTestimony updateTestimony, IFithubConfigHelper fithubConfigHelper)
        {
            _queryTestimony = queryTestimony ?? throw new ArgumentNullException(nameof(queryTestimony));
            _updateTestimony = updateTestimony ?? throw new ArgumentNullException(nameof(updateTestimony));
            this._fithubConfigHelper = fithubConfigHelper;
        }

        [HttpGet]
        //[Route("getUserTestimony/{UserID}")]
        [Route("getUserTestimony")] //test
        //[Authorize(Roles = "VIEWER")]
        public IActionResult GetUserTestimony([FromRoute] int UserID)
        {
            try
            {
                var result = _queryTestimony.QueryTestimonyByUser(_fithubConfigHelper.FithubConnectionString, UserID);
                if(result == null) { return NotFound(); } //test
                return Ok(result);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + exception.Message);
                throw;
            }

        }

        [HttpPost]
        [Route("createUserTestimony")]
        public IActionResult CreateUserTestimony([FromBody]TestimonyDTO testimony)
        {
            
            try
            {
                if (testimony == null) return BadRequest();

                int testimonyID = _updateTestimony.CreateTestimonyOfUser(_fithubConfigHelper.FithubConnectionString, testimony.Testimony, testimony.UserID);
                if(testimonyID>=0)
                {
                    testimony.TestimonyID=testimonyID;
                    return CreatedAtAction("GetUserTestimony", new { UserID = testimony.UserID },testimony);
                    
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + exception.Message);
                throw;
            }
        }

        [HttpPatch]
        [Route("updateUserTestimony/{testimonyID}")]
        public IActionResult UpdateUserTestimony([FromRoute] int testimonyID, [FromBody]TestimonyDTO testimony)
        {
            try
            {
                if (testimony == null) return BadRequest();

                if(_queryTestimony.QueryTestimonyByUser(_fithubConfigHelper.FithubConnectionString,testimony.UserID)==null) return NotFound();

                int result = _updateTestimony.UpdateTestimonyOfUser(_fithubConfigHelper.FithubConnectionString, testimony.Testimony, testimony.UserID, testimonyID);
                if (result >= 0)
                {
                    return NoContent();
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);

            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + exception.Message);
                throw;
            }
        }
    }
}
