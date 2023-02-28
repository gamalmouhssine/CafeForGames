using CafeForGames.Models;
using CafeForGames.Models.DTO;
using CafeForGames.Services.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CafeForGames.Controllers
{
    [Route("api/v{version:ApiVersion}/UsersAuth")]
    [ApiController]
    [ApiVersionNeutral]
    public class UsersController : ControllerBase
    {
        public IUserService _Service;
        protected ApiResponse _responce;
        public UsersController(IUserService Service)
        {
            _Service = Service;
            this._responce = new();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var response = await _Service.Login(loginRequestDTO);
            if (response.User == null || string.IsNullOrEmpty(response.Token))
            {
                _responce.ErrorMessages.Add("UserName Or Passowrd Incorrect");
                _responce.IsSuccess = false;
                _responce.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_responce);
            }
            _responce.IsSuccess = true;
            _responce.StatusCode = HttpStatusCode.OK;
            _responce.Result = response;
            return Ok(_responce);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            bool IsUniqueUser = _Service.IsUniqueUser(registerRequestDTO.UserName);
            if (!IsUniqueUser)
            {
                _responce.ErrorMessages.Add("User Already exists");
                _responce.IsSuccess = false;
                _responce.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_responce);
            }
            var user = await _Service.Register(registerRequestDTO);
            if (user ==null)
            {
                _responce.ErrorMessages.Add("error while registring");
                _responce.IsSuccess = false;
                _responce.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_responce);
            }
            _responce.StatusCode = HttpStatusCode.OK;
            _responce.IsSuccess = true;
            return Ok(_responce);
        }
    }
}
