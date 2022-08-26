using ERV.App.Infrastructure.Utils;
using ERV.App.Models.Enums;
using ERV.App.Models.ViewModels.Shared;
using ERV.App.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ERV.Web.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class PublicSecurityActionFilter : Attribute, IActionFilter
    {
        private readonly ISettingsService _settingsService;

        public PublicSecurityActionFilter(ISettingsService settingsService)
        {
            this._settingsService = settingsService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            var input = context.ActionArguments.FirstOrDefault(row => row.Value is ClientInputDTO dto).Value as ClientInputDTO;
            if (input == null || string.IsNullOrWhiteSpace(input.Id))
            {
                context.Result = new UnauthorizedObjectResult(AppResponse.Create(EAppResponse.InvalidInput));
                return;
            }

            var pageValidated = false;
            foreach (var key in context.ActionArguments.Keys)
            {
                if (!(context.ActionArguments[key] is ClientInputDTO dto))
                    continue;

                pageValidated = true;
                var isValidRequest = IsValidRequest(context.ActionArguments[key], dto.Id, dto.Token);

                dto.SetDefaults();
                if (!isValidRequest)
                    context.Result = new UnauthorizedObjectResult(isValidRequest);

                dto.Id = _settingsService.SiteInfo(input.Id).Id;
                break;
            }

            if (!pageValidated)
                context.Result = new UnauthorizedObjectResult(AppResponse.Create(EAppResponse.InvalidInput));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        { }
        #region Helper
        private bool IsValidRequest<T>(T value, string id, string clientToken) where T : class
        {
            try
            {
                if (value == null || string.IsNullOrWhiteSpace(clientToken))
                    return false;

                var info = _settingsService.SiteInfo(id);
                if (info == null)
                    return false;

                var serverToken = TokenEncrypter.Encrypt(value, info.SecretKey);
                if (serverToken != clientToken)
                    return false;

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion
    }
}
