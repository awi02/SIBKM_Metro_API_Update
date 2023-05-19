using API.Controllers.Base;
using API.Handler;
using API.Models;
using API.Repositories.Data;
using API.Repositories.Interface;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="admin")]
    public class AccountController : GeneralController<IAccountRepository, Account, string>
    {
        private readonly ITokenService _tokenService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        public AccountController(
            IAccountRepository repos,
            ITokenService tokenService,
            IEmployeeRepository employeeRepository, 
            IAccountRoleRepository accountRoleRepository) : base(repos)
        {
            _tokenService = tokenService;
            _employeeRepository = employeeRepository;
            _accountRoleRepository = accountRoleRepository;
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var register = _repos.Register(registerVM);
            if (register > 0)
            {
                return Ok(new ResponseDataVM<string>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Register Success"
                });
            }

            return BadRequest(new ResponseErrorsVM<string>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Errors = "Register Failed/Lost Connection"
            });
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult Login(loginVM loginVM)
        {
            var login = _repos.Login(loginVM);
            if (!login)
            {
                return NotFound(new ResponseErrorsVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Errors = "Login Failed,Account or  Password not found!"
                });

            }
            var claims = new List<Claim>()
            {
                new Claim("Email",loginVM.Email),
                new Claim("FullName", _employeeRepository.GetFullNameByEmail(loginVM.Email))
            };
            var getRoles = _accountRoleRepository.GetRolesByEmail(loginVM.Email);
            foreach(var role in getRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var token=_tokenService.GenerateToken(claims);
            return Ok(new ResponseDataVM<string>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Login Success",
                Data = token
            });
        }
    }
}