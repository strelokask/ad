using AD.DAL.Services.Base;
using AD.DAL.Services.Interfaces;
using AD.Domain.Models;
using AD.Domain.Settings.Options;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;

namespace AD.DAL.Services
{
    public class AdService : LoggingService, IAdService
    {
        private readonly AdOptions _adOptions;
        private readonly IMapper _mapper;

        public AdService(ILogger<AdService> logger, IOptions<AdOptions> options, IMapper mapper) : base(logger)
        {
            _adOptions = options.Value;
            _mapper = mapper;
        }

        public IEnumerable<AdUser> GetDomainUsers()
        {
            using (var context = new PrincipalContext(ContextType.Domain, _adOptions.Domain))
            {
                UserPrincipal userPrin = new UserPrincipal(context);
                userPrin.Enabled = true;
                userPrin.UserPrincipalName = $"*@*";
                using (var searcher = new PrincipalSearcher(userPrin))
                {
                    searcher.QueryFilter = userPrin;
                    var findAll = searcher.FindAll();
                    var castRes = findAll.Cast<UserPrincipal>();
                    var result = searcher.FindAll().Cast<UserPrincipal>()
                        .Select(up => _mapper.Map<UserPrincipal, AdUser>(up))
                        .ToList()
                        ;

                    return result;
                }
            }
        }
    }
}
