using ERV.App.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Services.Abstracts
{
    public interface ISettingsService
    {
        SiteInfo SiteInfo(string id);
        SiteInfo SiteInfo();
        JwtConfig JwtConfig();
        bool IsSiteRegistered(string id);
    }
}
