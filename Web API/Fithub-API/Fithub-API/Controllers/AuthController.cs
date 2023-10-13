using Fithub_API.Helper;
using Fithub_API.JWTFeature;
using Fithub_BL.Interfaces;
using Fithub_Data.DTO;
using Fithub_Data.DTO.ResponseDTO;
using Fithub_Data.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

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
        //[ProducesResponseType(StatusCodes.Status200OK, Type= typeof(AuthResponseDTO))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] AuthRequestDTO requestDTO)
        {
            if (requestDTO == null)
                return BadRequest(new { errorMessage = "User was not supplied" });

            try
            {


                var user = await _queryUser.QueryUserByEmail(_fithubConfigHelper.FithubConnectionString, requestDTO.Email);

                if (user == null)
                {
                    return NotFound(new { errorMessage = "No User found with this email, Please register using <a href='" + requestDTO.clientRegURL + "'>Register </a> Link" });
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

                return StatusCode((int)HttpStatusCode.InternalServerError, "Server was unable to process your request at this time, Try again later. - " + exception.Message);
            }


        }

        [HttpGet]
        [Route("getUserIdByEmail")]
        public IActionResult GetUserIdByEmail([FromQuery] string email)
        {
            if (email == null)
                return BadRequest(new { errorMessage = "Email is not supplied" });
            try
            {
                int userId = _queryUser.QueryUserIdByEmail(_fithubConfigHelper.FithubConnectionString, email);
                if (userId > 0)
                    return Ok(Convert.ToInt32(userId));
                else
                    return NotFound(new { errorMessage = "No User found with this email, Please register using <a href='/members/register'>Register </a> Link" });
            }
            catch (Exception exception)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, "Server was unable to process your request at this time, Try again later. - " + exception.Message);
            }

        }

        [HttpPost]
        [Route("resetUserPassword")]
        public IActionResult ResetUserPassword([FromBody] ResetPasswordDTO resetPasswordDto)
        {
            if (resetPasswordDto == null)
                return BadRequest(new { errorMessage = "Information was not supplied" });
            try
            {
                return Ok(_updateUser.ResetUserPassword(_fithubConfigHelper.FithubConnectionString, resetPasswordDto.userId, resetPasswordDto.password));
            }
            catch (Exception e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, "Server was unable to process your request at this time, Try again later. - " + e.Message);
            }

        }

        [HttpPost]
        [Route("externalGoogleLogin")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuthDTO externalAuthDTO)
        {
            if (externalAuthDTO == null)
                return BadRequest(new { errorMessage = "Information was not supplied" });

            try
            {
                var payload = _jWTHelper.VerifyGoogleToken(externalAuthDTO);
                if (payload == null)
                    return BadRequest(new { errorMessage = "Invalid external login" });

                // check if user exists in our DB
                var user = await _queryUser.QueryUserByEmail(_fithubConfigHelper.FithubConnectionString, payload.Email);
                if (user == null)
                {

                    int result = RegisterExternalUserFromExternalInformation(externalAuthDTO, payload, out user);
                    if (result < 0) return StatusCode((int)HttpStatusCode.InternalServerError, "Server was unable to process your request at this time, Try again later.");
                }


                var token = _jWTHelper.GenerateToken(user);
                return Ok(new AuthResponseDTO() { Token = token, User = user });
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Server was unable to process your request at this time, Try again later. - " + e.Message);
            }
        }



        [HttpPost]
        [Route("registerUser")]
        public IActionResult RegisterUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest(new { errorMessage = "User information was not supplied" });

            try
            {
                int result = _updateUser.RegisterUser(_fithubConfigHelper.FithubConnectionString, user);
                if (result >= 0) return Ok(new { success = true });
                else return StatusCode((int)HttpStatusCode.InternalServerError, "Server was unable to process your request at this time, Try again later.");
            }
            catch (Exception e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, "Server was unable to process your request at this time, Try again later. - " + e.Message);
            }
        }



        //Private helper Methods
        [NonAction]
        private int RegisterExternalUserFromExternalInformation(ExternalAuthDTO externalAuthDTO, GoogleJsonWebSignature.Payload payload, out User user)
        {
            var userToRegister = new User()
            {
                Email = payload.Email,
                Password = null, //TODO: do something intelligent here
                FirstName = payload.GivenName,
                LastName = payload.FamilyName,
                ExternalLoginProvider = externalAuthDTO.provider,
                ExternalLoginProviderName = externalAuthDTO.provider,
                IsExternalProvider = true,
                ExternalProviderKey = externalAuthDTO.idToken,
                Role = new UserRole() { Name = "Viewer", NormalisedName = "VIEWER" }
            };
            user = userToRegister;

            return _updateUser.RegisterUser(_fithubConfigHelper.FithubConnectionString, userToRegister);
        }

    }
}
