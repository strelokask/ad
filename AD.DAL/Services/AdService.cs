using AD.DAL.Services.Base;
using AD.DAL.Services.Interfaces;
using AD.Domain.Settings.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AD.DAL.Services
{
    public class AdService : LoggingService, IAdService
    {
        private readonly AdOptions _adOptions;

        public AdService(ILogger<AdService> logger, IOptions<AdOptions> options) : base(logger)
        {
            _adOptions = options.Value;
        }

        public void GetAdUsers()
        {
            _logger.LogInformation("Domain value {domain}", _adOptions.Domain);
        }
    }
}
