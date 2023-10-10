using Fithub_API.Helper;
using Fithub_BL.Interfaces;
using Fithub_Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Net;

namespace Fithub_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachController : ControllerBase
    {
        private readonly IQueryCoach _queryCoach;
        private readonly IUpdateCoach _updateCoach;
        private readonly LinkGenerator _linkGenerator;
        private readonly IFithubConfigHelper _fithubConfigHelper;

        public CoachController(IQueryCoach queryCoach, IUpdateCoach updateCoach, LinkGenerator linkGenerator, IFithubConfigHelper fithubConfigHelper)
        {
            _queryCoach = queryCoach ?? throw new ArgumentNullException(nameof(queryCoach));
            _updateCoach = updateCoach ?? throw new ArgumentNullException(nameof(updateCoach));
            _linkGenerator = linkGenerator ?? throw new ArgumentNullException(nameof(linkGenerator));
            this._fithubConfigHelper = fithubConfigHelper;
        }

        [HttpGet]
        [Route("getCoachesByClassID/{classID}")]
        public IActionResult GetCoachByClassID([FromRoute] int classID)
        {
            try
            {
                var listOfCoaches = _queryCoach.QueryCoachesByClassID(_fithubConfigHelper.FithubConnectionString, classID);
                return Ok(listOfCoaches);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + exception.Message);
            }

        }

        [HttpGet]
        [Authorize(Roles = "VIEWER")]
        [Route("getCoachByCoachID/{CoachID}")]
        public IActionResult GetCoachByCoachID([FromRoute] int CoachID)
        {
            try
            {
                var coach = _queryCoach.QueryCoachByCoachID(_fithubConfigHelper.FithubConnectionString, CoachID);
                return Ok(coach);
            }
            catch (Exception exception)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + exception.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "VIEWER")]
        [Route("getCoachRatingByUserID")]
        public IActionResult GetCoachRatingByUserID([FromQuery] int CoachID, [FromQuery] int UserID)
        {
            try
            {
                var coach = _queryCoach.QueryCoachByCoachID(_fithubConfigHelper.FithubConnectionString, CoachID);
                if (coach == null)
                    return NotFound();
                var rating = _queryCoach.QueryCoachRatingByUserID(_fithubConfigHelper.FithubConnectionString, CoachID, UserID);
                if (rating == null) return NotFound();
                else
                    return Ok(_queryCoach.QueryCoachRatingByUserID(_fithubConfigHelper.FithubConnectionString, CoachID, UserID));
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + exception.Message);
                throw;
            }

        }
        [HttpPost]
        [Route("addCoachRatingByUser")]
        [Authorize(Roles = "VIEWER")]
        public IActionResult AddCoachRatingByUserID([FromBody] Rating rating)
        {
            try
            {
                var result = _updateCoach.AddCoachRating(_fithubConfigHelper.FithubConnectionString, rating);
                if (result > 0)
                {
                    var location = _linkGenerator.GetPathByAction("GetCoachRatingByUserID", "Coach",
                        new { UserID = rating.UserID, CoachID = rating.CoachID });

                    return Created(location, rating);
                    //TODO: Use mapper here to return only the data that user should see
                    //not ratingID thatis of no use to user
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }

            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + exception.Message);
                throw;
            }


        }

        [HttpPut]
        [Authorize(Roles = "VIEWER")]
        [Route("updateCoachRatingByUser")]
        public IActionResult UpdateCoachRatingByUser([FromQuery] int CoachID, [FromQuery] int UserID, [FromBody] Rating rating)
        {
            try
            {
                var result = _updateCoach.UpdateCoachRating(_fithubConfigHelper.FithubConnectionString, CoachID, UserID, rating);
                if (result > 0)
                {
                    var location = _linkGenerator.GetPathByAction("GetCoachRatingByUserID", "Coach",
                        new { UserID = rating.UserID, CoachID = rating.CoachID });

                    return Created(location, rating);
                    //TODO: Use mapper here to return only the data that user should see
                    //not ratingID thatis of no use to user
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }

            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + exception.Message);
                throw;
            }

        }

        [HttpGet]
        [Route("getallcoachratingbyuser/{userID}")]
        [Authorize(Roles = "VIEWER")]
        public IActionResult GetAllCoachRatingByUser([FromRoute] int userID)
        {
            try
            {
                var ratings = _queryCoach.QueryCoachRatingsByUser(_fithubConfigHelper.FithubConnectionString, userID);
                return Ok(ratings);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + exception.Message);
                throw;
            }
        }
    }
}
