using System;
using System.Collections.Generic;
using System.Text;
using AD.Domain.Models;

namespace AD.DAL.Services.Interfaces
{
    public interface IAdService
    {
        IEnumerable<AdUser> GetDomainUsers();
    }
}
