using Fithub_API.Helper;
using Fithub_API.JWTFeature;
using Fithub_BL.Interfaces;
using Fithub_Data.DTO;
using Fithub_Data.DTO.ResponseDTO;
using Fithub_Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Fithub_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JWTHelper _jWTHelper;
        private readonly IQueryUser _queryUser;
        private readonly IUpdateUser _updateUser;
        private readonly IFithubConfigHelper _fithubConfigHelper;

        public AuthController(JWTHelper jWTHelper, IQueryUser queryUser, IUpdateUser updateUser, IFithubConfigHelper fithubConfigHelper)
        {
            _jWTHelper = jWTHelper ?? throw new ArgumentNullException(nameof(jWTHelper));
            _queryUser = queryUser ?? throw new ArgumentNullException(nameof(queryUser));
            _updateUser = updateUser ?? throw new ArgumentNullException(nameof(updateUser));
            this._fithubConfigHelper = fithubConfigHelper;
        }


        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] AuthRequestDTO requestDTO)
        {
            if (requestDTO == null)
                return BadRequest("User was not supplied");

            try
            {


                var user = _queryUser.QueryUserByEmail(_fithubConfigHelper.FithubConnectionString, requestDTO.Email);

                if (user == null)
                {
                    return Unauthorized(new { errorMessage = "No User found with this email, Please register using <a href='" + requestDTO.clientRegURL + "'>Register </a> Link" });
                }

                if (requestDTO.Password != user.Password)
                {
                    return Unauthorized(new { errorMessage = "Password do not match, To reset password click <a href='" + requestDTO.clientURL + "'>ForgotPassword</a>" });
                }


                var token = _jWTHelper.GenerateToken(user);
                return Ok(new AuthResponseDTO { User = user, Token = token });



            }
            catch (Exception exception)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + exception.Message);
            }


        }

        [HttpGet]
        [Route("getUserIdByEmail")]
        public IActionResult GetUserIdByEmail([FromQuery] string email)
        {
            if (email == null)
                return BadRequest("Email is not supplied");
            try
            {
                int userId = _queryUser.QueryUserIdByEmail(_fithubConfigHelper.FithubConnectionString, email);
                if (userId > 0)
                    return Ok(Convert.ToInt32(userId));
                else
                    return Unauthorized(new { errorMessage = "No User found with this email, Please register using <a href='/members/register'>Register </a> Link" });
            }
            catch (Exception exception)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + exception.Message);
            }

        }

        [HttpPost]
        [Route("resetUserPassword")]
        public IActionResult ResetUserPassword([FromBody] ResetPasswordDTO resetPasswordDto)
        {
            if (resetPasswordDto == null)
                return BadRequest("Information was not supplied");
            try
            {
                return Ok(_updateUser.ResetUserPassword(_fithubConfigHelper.FithubConnectionString, resetPasswordDto.userId, resetPasswordDto.password));
            }
            catch (Exception e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + e.Message);
            }

        }

        [HttpPost]
        [Route("externalGoogleLogin")]
        public IActionResult ExternalLogin([FromBody] ExternalAuthDTO externalAuthDTO)
        {
            if (externalAuthDTO == null)
                return BadRequest("Information was not supplied");

            try
            {
                var payload = _jWTHelper.VerifyGoogleToken(externalAuthDTO);
                if (payload == null)
                    return BadRequest("Invalid external login");

                //TODO: make this perfrect. at this point of time I am keeping this as simple
                var token = _jWTHelper.GenerateToken(new User() { Email = payload.Email });
                return Ok(new { Token = token });
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + e.Message);
            }
        }
        [HttpPost]
        [Route("registerUser")]
        public IActionResult RegisterUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("User not supplied");

            try
            {
                int result = _updateUser.RegisterUser(_fithubConfigHelper.FithubConnectionString, user);
                if (result >= 0) return Ok(new { success = true });
                else return BadRequest("there was some problem");
            }
            catch (Exception e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, "Something bad happened - " + e.Message);
            }
        }



    }
}
