using API.Models;
using API.Repositories;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class GeneralController<TRepos, TEntity, TKey> : ControllerBase
    where TRepos : IGeneralRepos<TEntity, TKey>
    {
        protected TRepos _repos;
        public GeneralController(TRepos repos)
        {
            _repos = repos;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var results = _repos.GetAll();
            if (results == null)
                return NotFound(new ResponseErrorsVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Errors = "Id Not Found"
                });
            return Ok(new ResponseDataVM<IEnumerable<TEntity>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = results
            });
        }
        [HttpGet("{Key}")]
        public ActionResult GetByKey(TKey key) 
        {
            var results = _repos.GetByKEY(key);
            if (results == null)
                return NotFound(new ResponseErrorsVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Errors = "Id Not Found"
                });
            return Ok(new ResponseDataVM<TEntity>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = results
            });
        }

        [HttpPost]
        public ActionResult Insert(TEntity entity)
        {
            /*if (string.IsNullOrWhiteSpace(account.Password) || string.IsNullOrWhiteSpace(account.Password))
            {
                return BadRequest(new ResponseErrorsVM<string>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Errors = "Value Cannot be Null or Default"
                });
            }*/
            var insert = _repos.Insert(entity);
            if (insert > 0)
            {
                return Ok(new ResponseDataVM<TEntity>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Insert Success",
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
        public ActionResult Update(TEntity entity)
        {
            /*if (string.IsNullOrWhiteSpace(account.Password) || string.IsNullOrWhiteSpace(account.Password))
            {
                return BadRequest(new ResponseErrorsVM<string>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Errors = "Value Cannot be Null or Default"
                });
            }*/
            var update = _repos.Update(entity);
            if (update > 0)
            {
                return Ok(new ResponseDataVM<TEntity>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Update Success",
                });
            }
            return BadRequest(new ResponseErrorsVM<string>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Errors = "Update Failed / Lost Connection"
            });

        }
        [HttpDelete("{key}")]
        public ActionResult Delete(TKey key)
        {
            var delete = _repos.Delete(key);
            if (delete > 0)
            {
                return Ok(new ResponseDataVM<TEntity>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Delete Success",
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
