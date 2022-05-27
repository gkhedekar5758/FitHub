using Fithub_BL.Interfaces;
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
  public class CoachController : ControllerBase
  {
    private readonly IQueryCoach _queryCoach;

    public CoachController(IQueryCoach queryCoach)
    {
      _queryCoach = queryCoach??throw new ArgumentNullException(nameof(queryCoach));
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
  }
}
