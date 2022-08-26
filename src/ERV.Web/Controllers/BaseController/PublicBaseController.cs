using ERV.App.Infrastructure.Utils;
using ERV.App.Models.Enums;
using ERV.App.Models.ViewModels.Shared;
using ERV.Web.Utils.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace ERV.Web.Controllers.BaseController
{
    public class PublicBaseController : Controller
    {
        protected readonly WebAppSettings AppSettings;
        protected readonly IApiClientService ApiClient;

        public PublicBaseController(IOptions<WebAppSettings> settings, IApiClientService apiClient)
        {
            AppSettings = settings.Value;
            LocalizationManager = new LocalizationManager(Resources.Lang.ResourceManager);
            ApiClient = apiClient;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(CultureTwoLetterName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            ViewBag.Title = AppSettings.Name;

            ViewBag.CultureTwoLetterName = CultureTwoLetterName;
            await base.OnActionExecutionAsync(context, next);
        }

        public LocalizationManager LocalizationManager { get; private set; }

        protected virtual string Lang(string name)
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;

            return LocalizationManager.GetString(name, cultureInfo);
        }

        public string CultureThreeLetterName => (CultureTwoLetterName == "EN") ? "ENG" : "SPA";

        public string CultureTwoLetterName
        {
            get
            {
                var currentLanguage = Request.Cookies["CurrentLanguage"];
                return !string.IsNullOrWhiteSpace(currentLanguage) && currentLanguage == "ES" ? "ES" : "EN";
            }
            set
            {
                var cookie = Request.Cookies["CurrentLanguage"];
                var newcookie = new CookieOptions();
                if (cookie != null)
                    cookie = value;
                else
                {
                    newcookie.Expires = DateTime.Now.AddYears(1);
                }

                Response.Cookies.Append("CurrentLanguage", value, newcookie);
            }
        }
        public string IpAddress
        {
            get
            {
                var ipAddress = new System.Net.IPAddress(default(long));
                try
                {
                    ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
                }
                catch (Exception ex)
                {
                    ipAddress = new System.Net.IPAddress(default(long));
                }

                return ipAddress.ToString();
            }
        }

        public string AbsoluteBasePath(string path)
        {
            return AppSettings.BaseUrl.Substring(0, AppSettings.BaseUrl.Length - 1) + path;
        }

        protected async Task<ViewResult> Error(EErrorCodeClientApi section, Exception ex)
        {
            return View("~/Views/Shared/Partials/_Error.cshtml", section);
        }

        public ClientInputDTO InputParameter=> new ClientInputDTO
        {
            Id = AppSettings.Id,
            //IpAddressClient = IpAddress,
            //IpAddressService = IpAddress,
            Token = string.Empty
        };    
    }
}
