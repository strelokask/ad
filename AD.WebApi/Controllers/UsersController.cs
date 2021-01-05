using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AD.DAL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAdService _adService;

        public UsersController(IAdService adService)
        {
            _adService = adService;
        }

        [HttpGet]
        public IActionResult Fetch() {
            var domainUsers = _adService.GetDomainUsers();

            return Ok(domainUsers);
        }
    }
}
