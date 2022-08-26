using ERV.App.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.DataAccess.Repositories.Abstracts
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetCountries();
        Task<Country> GetCountryDetails(string countryCode);
        Task<List<Country>> GetRegionDetails(string region);
        Task<List<Country>> GetSubRegions(string subRegionName);
    }
}
