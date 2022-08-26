using ERV.App.Infrastructure.Utils;
using ERV.App.Services.Abstracts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly WebApiAppSettings settings;

        public SettingsService(IOptions<WebApiAppSettings> settings)
        {
            this.settings = settings.Value;
        }

        public SiteInfo SiteInfo(string id)
        {
            return settings.SiteInfos.FirstOrDefault(row => row.Id == id);
        }
        public SiteInfo SiteInfo()
        {
            return settings.SiteInfos.FirstOrDefault();
        }

        public bool IsSiteRegistered(string id)
        {
            var isPrincipal = settings.SiteInfos.Any(row => string.Equals(row.Id, id, StringComparison.InvariantCultureIgnoreCase));
            if (isPrincipal) return false;

            return true;
        }
        public JwtConfig JwtConfig()
        {
            return settings.JwtConfig;
        }
    }
}
