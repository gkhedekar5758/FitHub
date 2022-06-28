using Fithub_BL.Interfaces;
using Fithub_Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Fithub_API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CoachController : ControllerBase
  {
    private readonly IQueryCoach _queryCoach;
        private readonly IUpdateCoach _updateCoach;
        private readonly LinkGenerator _linkGenerator;

        public CoachController(IQueryCoach queryCoach,IUpdateCoach updateCoach,LinkGenerator linkGenerator)
        {
            _queryCoach = queryCoach??throw new ArgumentNullException(nameof(queryCoach));
            _updateCoach = updateCoach??throw new ArgumentNullException(nameof(updateCoach));
            _linkGenerator = linkGenerator?? throw new ArgumentNullException(nameof(linkGenerator));
        }

    [HttpGet]
    [Route("getCoachesByClassID/{classID}")]
    public IActionResult GetCoachByClassID([FromRoute]int classID)
    {
      try
      {
        var listOfCoaches = _queryCoach.QueryCoachesByClassID(classID);
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
    public IActionResult GetCoachByCoachID([FromRoute]int CoachID)
    {
      try
      {
        var coach = _queryCoach.QueryCoachByCoachID(CoachID);
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
    public IActionResult GetCoachRatingByUserID([FromQuery]int CoachID,[FromQuery]int UserID)
    {
      try
      {
        return Ok(_queryCoach.QueryCoachRatingByUserID(CoachID, UserID));
      }
      catch (Exception exception)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + exception.Message);
        throw;
      }
      
    }
        [HttpPost]
        [Route("addCoachRatingByUser")]
        [Authorize(Roles ="VIEWER")]
        public IActionResult AddCoachRatingByUserID([FromBody]Rating rating)
        {
            try
            {
                var result = _updateCoach.AddCoachRating(rating);
                if(result >0)
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
        public IActionResult UpdateCoachRatingByUser([FromQuery]int CoachID,[FromQuery]int UserID,[FromBody]Rating rating)
        {
            try
            {
                var result = _updateCoach.UpdateCoachRating(CoachID,UserID,rating);
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
    }
}
