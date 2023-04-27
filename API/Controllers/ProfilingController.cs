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
    public class ProfilingController : ControllerBase
    {
        private readonly IProfilingRepository _profilingRepository;
        public ProfilingController(IProfilingRepository profilingRepository)
        {
            _profilingRepository = profilingRepository;
        }

        [HttpGet]
        public ActionResult profiling()
        {
            var profilings = _profilingRepository.GetAll();
            return Ok(new ResponseDataVM<IEnumerable<Profiling>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = profilings
            });
        }
        [HttpGet("(Id)")]
        public ActionResult Get(string id)
        {
            var profiling = _profilingRepository.GetById(id);
            if (profiling == null)
            {
                return NotFound(new ResponseErrorsVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Errors = "Id Not Found"
                });
            }
            return Ok(new ResponseDataVM<Profiling>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = profiling
            });
            }
        [HttpPost]
        public ActionResult Insert(Profiling profiling)
        {
            if (string.IsNullOrEmpty(profiling.EmployeeNIK))
            {
                return BadRequest(new ResponseErrorsVM<string>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Errors = "Value Cannot be Null or Default"
                });
            }
            var insert = _profilingRepository.Insert(profiling);
            if (insert > 0)
            {
                return Ok(new ResponseDataVM<Profiling>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Insert Success",
                    Data = null!
                });
            }
            return BadRequest(new ResponseErrorsVM<string>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Errors = "Insert Failed / Lost Connection"
            });
        }
        [HttpPut]
        public ActionResult Update(Profiling profiling)
        {
            if (string.IsNullOrEmpty(profiling.EmployeeNIK))
            {
                return BadRequest(new ResponseErrorsVM<string>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Errors = "Value Cannot be Null or Default"
                });
            }
            var update = _profilingRepository.Update(profiling);
            if (update > 0)
            {
                return Ok(new ResponseDataVM<Profiling>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Update Success",
                    Data = null!
                });
            }
            return BadRequest(new ResponseErrorsVM<string>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Errors = "Update Failed / Lost Connection"
            });
        }
        [HttpDelete("{Id}")]
        public ActionResult Delete(string id)
        {
            var delete = _profilingRepository.Delete(id);
            if (delete > 0)
            {
                return Ok(new ResponseDataVM<Profiling>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Delete Success",
                    Data = null!
                });

            }
            return BadRequest(new ResponseErrorsVM<string>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Errors = "Delete Failed / Lost Connection"
            });
        }
    }
}

