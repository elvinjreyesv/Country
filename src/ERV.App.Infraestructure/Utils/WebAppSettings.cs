using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Infrastructure.Utils
{
    public class WebAppSettings
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string WebApiBaseUrl { get; set; }
        public string BaseUrl { get; set; }
        public string SecretKey { get; set; }
    }
}
