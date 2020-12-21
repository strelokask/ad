using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AD.WebApi.Controllers
{
    public class UserSearcherInfo
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
    }
}
