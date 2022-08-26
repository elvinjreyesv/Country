using ERV.App.Models.Enums;
using ERV.App.Models.ViewModels.Public.Inputs;
using ERV.App.Models.ViewModels.Public.Outputs;
using ERV.App.Models.ViewModels.Shared;
using ERV.App.Services.Abstracts;
using ERV.Web.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERV.Web.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [TypeFilter(typeof(PublicSecurityActionFilter), Order = 1)]
    [TypeFilter(typeof(PublicPostSecurityActionFilter), Order = 2)]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        [Produces(typeof(List<CountryInfoDTO>))]
        public async Task<ActionResult<List<CountryInfoDTO>>> Get([FromQuery] ClientInputDTO dto)
        {
            var content = await _countryService.Countries();
            return Ok(content);
        }

        [HttpGet("Country")]
        [Produces(typeof(AppResponse<EAppResponse, CountryDTO>))]
        public async Task<ActionResult<AppResponse<EAppResponse, CountryDTO>>> Country([FromQuery] CountryInputDTO input)
        {
            try
            {
                var content = await _countryService.CountryInformation(input.Code);
                return Ok(content);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("Region")]
        [Produces(typeof(AppResponse<EAppResponse, RegionDTO>))]
        public async Task<ActionResult<AppResponse<EAppResponse, RegionDTO>>> Region([FromQuery] RegionInputDTO input)
        {
            var content = await _countryService.RegionInformation(input.Name);
            return Ok(content);
        }

        [HttpGet("SubRegion")]
        [Produces(typeof(AppResponse<EAppResponse, SubRegionDTO>))]
        public async Task<ActionResult<AppResponse<EAppResponse, SubRegionDTO>>> SubRegion([FromQuery] RegionInputDTO input)
        {
            var content = await _countryService.SubRegionInformation(input.Name);
            return Ok(content);
        }
    }
}
