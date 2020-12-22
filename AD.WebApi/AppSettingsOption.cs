using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AD.WebApi
{
    public class AppSettingsOption
    {
        public AdSettings AdSettings { get; set; }
    }
    public class AdSettings {
        public string Domain { get; set; }
    }
}
