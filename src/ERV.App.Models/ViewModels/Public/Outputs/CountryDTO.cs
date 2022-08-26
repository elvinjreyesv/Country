using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Models.ViewModels.Public.Outputs
{
    public class CountryDTO: CountryInfoDTO
    {
        public List<string> CapitalCities { get; set; }
        public int Population { get; set; }
        public List<CurrencyDTO> Currencies { get; set; }
        public List<LanguageDTO> Languages { get; set; }
        public List<CountryInfoDTO> BorderCountries { get; set; }
    }

    public class CurrencyDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }

    public class LanguageDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
