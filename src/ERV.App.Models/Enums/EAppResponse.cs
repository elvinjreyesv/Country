using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Models.Enums
{
    public enum EAppResponse
    {
        InvalidInput,
        Success,
        Error,
        NoResults,
        UnhandledError,
        UnableComplete,
        InvalidToken
    }
}
