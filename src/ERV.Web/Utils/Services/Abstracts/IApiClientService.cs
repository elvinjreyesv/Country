using ERV.App.Models.Enums;
using ERV.App.Models.ViewModels.Public.Inputs;
using ERV.App.Models.ViewModels.Public.Outputs;
using ERV.App.Models.ViewModels.Shared;

namespace ERV.Web.Utils.Services.Abstracts
{
    public interface IApiClientService
    {
        Task<List<CountryInfoDTO>> GetCountries(ClientInputDTO dto, string secretKey);
        Task<AppResponse<EAppResponse, CountryDTO>> GetCountry(CountryInputDTO dto, string secretKey);
        Task<AppResponse<EAppResponse, RegionDTO>> GetRegion(RegionInputDTO dto, string secretKey);
        Task<AppResponse<EAppResponse, SubRegionDTO>> GetSubRegion(RegionInputDTO dto, string secretKey);
    }
}
