using ERV.App.Models.Entities;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.ConnectedServices.Countries
{
    public interface IApiMethods
    {
        [Get("/all")]
        Task<List<Country>> GetCountries();

        [Get("/alpha/{code}")]
        Task<List<Country>> GetCountryDetails(string code);

        [Get("/region/{region}")]
        Task<List<Country>> GetRegionDetails(string region);

        [Get("/subregion/{region}")]
        Task<List<Country>> GetSubRegions(string region);
    }
}
