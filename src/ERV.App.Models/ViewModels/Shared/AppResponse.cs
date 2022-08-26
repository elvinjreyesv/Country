using ERV.App.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Models.ViewModels.Shared
{
    public class AppResponse : AppResponse<string>
    {
        public static AppResponse Create(EAppResponse status) => new AppResponse() { Status = status };

        public static AppResponse<TStatusEntry, string> CreateByStatus<TStatusEntry>(TStatusEntry status, string value = "") where TStatusEntry : struct
        {
            return new AppResponse<TStatusEntry, string> { Status = status, Result = value };
        }
    }

    public class AppResponse<TEntity> : AppResponse<EAppResponse, TEntity>
    {
        public static AppResponse<TEntityEntry> Create<TEntityEntry>(EAppResponse status, TEntityEntry value) => new AppResponse<TEntityEntry> { Status = status, Result = value };
        public static AppResponse<TEntityEntry> Create<TEntityEntry>(EAppResponse status) => new AppResponse<TEntityEntry> { Status = status, Result = default(TEntityEntry) };
    }

    public class AppResponse<TStatus, TEntity>
    {
        public TStatus Status { get; set; }
        public TEntity Result { get; set; }

        public static AppResponse<TStatusEntry, TEntityEntry> Create<TStatusEntry, TEntityEntry>(TStatusEntry status, TEntityEntry value)
        {
            return new AppResponse<TStatusEntry, TEntityEntry> { Status = status, Result = value };
        }
        public static AppResponse<TStatusEntry, TEntityEntry> Create<TStatusEntry, TEntityEntry>(TStatusEntry status) where TStatusEntry : struct
        {
            return new AppResponse<TStatusEntry, TEntityEntry> { Status = status, Result = default(TEntityEntry) };
        }
        public static AppResponse<TStatusEntry, List<TEntityEntry>> CreateEmptyList<TStatusEntry, TEntityEntry>(TStatusEntry status)
            where TStatusEntry : struct
        {
            return new AppResponse<TStatusEntry, List<TEntityEntry>> { Status = status, Result = Enumerable.Empty<TEntityEntry>().ToList() };
        }
    }

    public class AppResponseAjax
    {
        public bool Success { get; protected set; }
        public AppResponseAjaxTarget TargetUrl { get; protected set; }
        public bool UnauthorizedRequest { get; protected set; }
        public object Result { get; protected set; }
        public AppResponseAjaxError Error { get; protected set; }
        public AppResponseAjax()
        {
            Success = false;
            TargetUrl = new AppResponseAjaxTarget();
            UnauthorizedRequest = false;
            Result = new object();
            Error = new AppResponseAjaxError();
        }

        public static AppResponseAjax Create(bool success, object result)
        {
            return new AppResponseAjax() { Success = success, Result = result };
        }
        public static AppResponseAjax CreateTarget(string targetUrl, bool external)
        {
            return new AppResponseAjax() { Success = true, TargetUrl = new AppResponseAjaxTarget() { External = external, Url = targetUrl } };
        }
        public static AppResponseAjax CreateSuccess(object result)
        {
            return new AppResponseAjax() { Success = true, Result = result };
        }
        public static AppResponseAjax CreateError(string error)
        {
            return new AppResponseAjax() { Success = false, Error = new AppResponseAjaxError() { Message = error } };
        }
        public static AppResponseAjax CreateError(string error, EStatusAjax status)
        {
            return new AppResponseAjax() { Success = false, Error = new AppResponseAjaxError() { Message = error, Status = status } };
        }
    }

    public class AppResponseAjaxError
    {
        public string Message { get; set; }
        public object Details { get; set; }
        public EStatusAjax Status { get; set; }
        public AppResponseAjaxError()
        {
            Message = string.Empty;
            Details = string.Empty;
            Status = EStatusAjax.Error;
        }
    }
    public class AppResponseAjaxTarget
    {
        public string Url { get; set; }
        public bool External { get; set; }
        public AppResponseAjaxTarget()
        {
            Url = string.Empty;
            External = true;
        }
    }
}
