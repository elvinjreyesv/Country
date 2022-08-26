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
    public class HomeController : PublicBaseController
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(IOptions<WebAppSettings> settings, IApiClientService apiClient, ILogger<HomeController> logger) : base(settings, apiClient)
        {
            _logger = logger;
        }

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

        [HttpPost]
        public IActionResult ChangeLanguage(string returnUrl, string newLang)
        {
            if (string.IsNullOrWhiteSpace(newLang) || newLang == CultureTwoLetterName)
                return Json(AppResponseAjax.CreateTarget(returnUrl, false));

            CultureTwoLetterName = newLang;
            return Json(AppResponseAjax.CreateTarget(returnUrl, false));
        }
    }
}