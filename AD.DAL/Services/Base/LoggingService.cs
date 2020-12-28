using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AD.DAL.Services.Base
{
    public class LoggingService
    {
        protected readonly ILogger _logger;

        public LoggingService(ILogger logger)
        {
            _logger = logger;
        }
    }
}
