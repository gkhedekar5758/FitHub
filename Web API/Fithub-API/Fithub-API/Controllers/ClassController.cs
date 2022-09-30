using Fithub_API.Helper;
using Fithub_BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IFithubConfigHelper _fithubConfigHelper;

        public ClassController(IQueryClass queryClass,IFithubConfigHelper fithubConfigHelper)
    {
      _queryClass = queryClass ?? throw new ArgumentNullException(nameof(queryClass));
            this._fithubConfigHelper = fithubConfigHelper?? throw new ArgumentNullException(nameof(fithubConfigHelper));
        }

    [HttpGet]
    [Route("getClasses")]
    public IActionResult GetClasses()
    {
      try
      {
        var listOfClass = _queryClass.QueryClasses(_fithubConfigHelper.FithubConnectionString);
        return Ok(listOfClass);

      }
      catch (Exception exception)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + exception.Message);
        
      }
    }
  }
}
