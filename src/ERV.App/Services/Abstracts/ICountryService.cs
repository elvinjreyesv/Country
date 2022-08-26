using ERV.App.Models.Enums;
using ERV.App.Models.ViewModels.Public.Outputs;
using ERV.App.Models.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Services.Abstracts
{
    public interface ICountryService
    {
        Task<List<CountryInfoDTO>> Countries();
        Task<AppResponse<EAppResponse, CountryDTO>> CountryInformation(string countryCode);
        Task<AppResponse<EAppResponse, RegionDTO>> RegionInformation(string regionName);
        Task<AppResponse<EAppResponse, SubRegionDTO>> SubRegionInformation(string subRegionName);
    }
}
