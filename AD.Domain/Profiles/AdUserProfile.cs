using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Text;
using AD.Domain.Models;
using AutoMapper;

namespace AD.Domain.Profiles
{
    public class AdUserProfile : Profile
    {
        public AdUserProfile()
        {
            CreateMap<UserPrincipal, AdUser>();
        }
    }
}
