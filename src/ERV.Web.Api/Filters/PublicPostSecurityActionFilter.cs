using ERV.App.Infrastructure.Utils;
using ERV.App.Models.ViewModels.Shared;
using ERV.App.Services.Abstracts;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ERV.Web.Api.Filters
{
    public class PublicPostSecurityActionFilter : Attribute, IActionFilter
    {
        private readonly ISettingsService _settingsService;
        public PublicPostSecurityActionFilter(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var key in context.ActionArguments.Keys)
            {
                if (!(context.ActionArguments[key] is ClientInputDTO dto)) continue;

                if (!(_settingsService.SiteInfo(dto.Id) is SiteInfo info)) continue;

                dto.Id = info.Id;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        { }
    }
}
