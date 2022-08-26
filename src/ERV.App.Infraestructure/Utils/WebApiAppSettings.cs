using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Infrastructure.Utils
{
    public class WebApiAppSettings
    {
        public List<SiteInfo> SiteInfos { get; set; }
        public List<ExternalServices> ExternalServices { get; set; }
        public JwtConfig JwtConfig { get; set; }
        public CacheConfiguration CacheConfiguration { get; set; }
    }
    public class JwtConfig
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string SigningKey { get; set; }
    }
    public class SiteInfo
    {
        public string Id { get; set; }
        public string SecretKey { get; set; }
    }
    public class ExternalServices
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public string BaseUrl { get; set; }
    }

    public class CacheConfiguration
    {
        public int AbsoluteExpirationSeconds { get; set; }
        public int SlidingExpirationSeconds { get; set; }
    }
}
