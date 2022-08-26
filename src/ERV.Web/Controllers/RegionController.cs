using ERV.App.Infrastructure.Utils;
using ERV.App.Models.Enums;
using ERV.Web.Controllers.BaseController;
using ERV.Web.Utils.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using ERV.App.Models.ViewModels.Shared;
using ERV.App.Models.ViewModels.Public.Inputs;
using ERV.App.Models.ViewModels.Public.Outputs;
using ERV.Web.Utils.Extensions;
using ERV.App.Infrastructure.Utils.Constants;
using X.PagedList;

namespace ERV.Web.Controllers
{
    [Controller]
    public class RegionController : PublicBaseController
    {
        private readonly ILogger<HomeController> _logger;
        public RegionController(IOptions<WebAppSettings> settings, IApiClientService apiClient, ILogger<HomeController> logger) : base(settings, apiClient)
        {
            _logger = logger;
        }

        [HttpGet("Region/{name}")]
        public async Task<IActionResult> Index([FromRoute] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData.Put(TempDataKeyConstants.ErrorMessage, Lang("_NoRegion"));
                return View(new RegionDTO());
            }

            try
            {
                var input = new RegionInputDTO()
                {
                    Name = name,
                    Id = InputParameter.Id
                };

                var result = await ApiClient.GetRegion(input, AppSettings.SecretKey);

                if (result.Status != EAppResponse.Success)
                    TempData.Put(TempDataKeyConstants.ErrorMessage, Lang("_NoResults"));

                return View(result.Result);
            }
            catch (Exception ex)
            {
                return await Error(EErrorCodeClientApi.HomeController, ex);
            }
        }

        [HttpGet("Region/{region}/SubRegion/{subregion}")]
        public async Task<IActionResult> SubRegion([FromRoute] string region, string subregion)
        {
            if (string.IsNullOrWhiteSpace(subregion))
            {
                TempData.Put(TempDataKeyConstants.ErrorMessage, Lang("_NoRegion"));
                return View(new SubRegionDTO());
            }

            try
            {
                var input = new RegionInputDTO()
                {
                    Name = subregion,
                    Id = InputParameter.Id
                };

                var result = await ApiClient.GetSubRegion(input, AppSettings.SecretKey);
                if (result.Status != EAppResponse.Success)
                    TempData.Put(TempDataKeyConstants.ErrorMessage, Lang("_NoResults"));

                return View(result.Result);
            }
            catch (Exception ex)
            {
                return await Error(EErrorCodeClientApi.HomeController, ex);
            }
        }
    }
}