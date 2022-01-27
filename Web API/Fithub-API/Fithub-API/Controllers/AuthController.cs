using Fithub_Data.Models;
using Microsoft.AspNetCore.Mvc;
using Fithub_API.JWTFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace Fithub_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly JWTHelper _jWTHelper;
    public AuthController(JWTHelper jWTHelper)
    {
      _jWTHelper = jWTHelper;
    }


    [Route("login")]
    [HttpPost]
    public IActionResult Login(UserModel user)
    {
      if (user == null)
        return BadRequest("User was not supplied");

      try
      {
       
        //TODO: ideally fetch the user
        //hardcoding as of now
        if (user.Email == "guk@guk.com" && user.Password == "abc")
        {
          var token =  _jWTHelper.GenerateToken(user);
          return Ok(new { Token = token });


        }
        else
        {
          return Unauthorized();
        }
      }
      catch (Exception exception)
      {

        return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + exception.Message);
      }
      

    }
  }
}
