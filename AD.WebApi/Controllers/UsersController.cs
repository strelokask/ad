using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AD.WebApi.Controllers
{
    [Route("hr/api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Search(int take, string login, string query)
        {
            var users = new UserSearcherInfo[take];
            for (int i = 0; i < take; i++) { 
                var usi = new UserSearcherInfo()
                {
                    Department = "Портал ОС",
                    Email = "ivanov.ivan@x5.ru",
                    JobTitle = "Ведущий разработчик",
                    Login = login,
                    Phone = "+71231231231",
                    Title = "Иванов Иван"
                };
                if (string.IsNullOrEmpty(login))
                    usi.Login = query;
                users[i] = usi;
            }
            return Ok(new
            {
                Skipped = 0,
                Total = take,
                Items = users
            });
        }
    }
}
