using ERV.App.Models.Enums;
using ERV.App.Models.ViewModels.Public.Outputs;
using ERV.App.Models.ViewModels.Shared;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Infrastructure.Utils.RefitInterfaces
{
    public interface IApiMethods
    {
        [Get("/api/v1/Countries/?Id={id}&Token={token}")]
        Task<List<CountryInfoDTO>> GetCountries(string id, string token);

        [Get("/api/v1/Countries/Country?Code={countryCode}&Id={id}&Token={token}")]
        Task<AppResponse<EAppResponse, CountryDTO>> GetCountry(string countryCode, string id, string token);

        [Get("/api/v1/Countries/Region?Name={regionName}&Id={id}&Token={token}")]
        Task<AppResponse<EAppResponse, RegionDTO>> GetRegion(string regionName, string id, string token);

        [Get("/api/v1/Countries/SubRegion?Name={regionName}&Id={id}&Token={token}")]
        Task<AppResponse<EAppResponse, SubRegionDTO>> GetSubRegion(string regionName, string id, string token);
    }
}
