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
    public class CountryController : PublicBaseController
    {
        private readonly ILogger<HomeController> _logger;
        public CountryController(IOptions<WebAppSettings> settings, IApiClientService apiClient, ILogger<HomeController> logger) : base(settings, apiClient)
        {
            _logger = logger;
        }

        [HttpGet("Country")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await ApiClient.GetCountries(InputParameter, AppSettings.SecretKey);

                return View(result);
            }
            catch (Exception ex)
            {
                return await Error(EErrorCodeClientApi.HomeController, ex);
            }
        }

        [HttpGet("Country/{code}")]
        public async Task<IActionResult> Country([FromRoute] string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                TempData.Put(TempDataKeyConstants.ErrorMessage, Lang("_NoCountrySelected"));
                return View(new CountryDTO());
            }

            try
            {
                var input = new CountryInputDTO()
                {
                    Code = code,
                    Id = InputParameter.Id
                };

                var result = await ApiClient.GetCountry(input, AppSettings.SecretKey);
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