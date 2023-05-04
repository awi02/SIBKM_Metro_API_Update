using API.Controllers.Base;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : GeneralController<IAccountRepository, Account, string>
    {
        public AccountController(IAccountRepository repos) : base(repos)
        {
        }
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
                Errors = "Register Failed"
            });
        }
        [HttpPost("Login")]
        public ActionResult Login(loginVM loginVM)
        {
            var login = _repos.Login(loginVM);
            if (login)
            {
                return Ok(new ResponseDataVM<string>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Login Success",
                });

            }

            return BadRequest(new ResponseErrorsVM<string>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Errors = "Login Failed"
            });
        }
    }
}