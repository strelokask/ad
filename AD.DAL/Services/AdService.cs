using AD.DAL.Services.Base;
using AD.DAL.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AD.DAL.Services
{
    public class AdService : LoggingService, IAdService
    {
        private readonly IConfiguration _config;

        public AdService(ILogger<AdService> logger, IConfiguration config) : base(logger)
        {
            _config = config;
        }

        public void GetAdUsers()
        {
            for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++) {
                _logger.LogInformation("Run number {runNumber}", i);
            }
        }
    }
}
