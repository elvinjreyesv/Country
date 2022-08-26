using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Infrastructure.Utils
{
    public interface ILocalizationManager
    {
        string GetString(string name);
        string GetString(string name, CultureInfo culture);
        string GetStringOrNull(string name, bool tryDefaults = true);
    }
    public class LocalizationManager : ILocalizationManager
    {
        private readonly ResourceManager _resourceManager;
        public LocalizationManager(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }
        public string GetString(string name)
        {
            return _resourceManager.GetString(name);
        }
        public string GetString(string name, CultureInfo culture)
        {
            return _resourceManager.GetString(name, culture) ?? "";
        }
        public string GetStringOrNull(string name, bool tryDefaults = true)
        {
            return _resourceManager.GetString(name) ?? (tryDefaults ? "" : null);
        }
    }
}
