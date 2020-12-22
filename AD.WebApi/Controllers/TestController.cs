using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AD.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly AppSettingsOption _appSettings;

        public TestController(ILogger<TestController> logger, IOptions<AppSettingsOption> options)
        {
            _logger = logger;
            _appSettings = options.Value;
        }

        [HttpGet]
        public AppSettingsOption Get()
        {
            return (_appSettings);
        }
    }
}
