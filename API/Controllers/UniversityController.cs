﻿using API.Controllers.Base;
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
    public class UniversityController : GeneralController<IUniversityRepository, University, int>
    {
        public UniversityController(IUniversityRepository repos) : base(repos)
        {
        }
    }
}
