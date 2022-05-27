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
  public class ClassController : ControllerBase
  {
    private readonly IQueryClass _queryClass;
    public ClassController(IQueryClass queryClass)
    {
      _queryClass = queryClass ?? throw new ArgumentNullException(nameof(queryClass));
    }

    [HttpGet]
    [Route("getClasses")]
    public IActionResult GetClasses()
    {
      try
      {
        var listOfClass = _queryClass.QueryClasses();
        return Ok(listOfClass);

      }
      catch (Exception exception)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + exception.Message);
        
      }
    }
  }
}
