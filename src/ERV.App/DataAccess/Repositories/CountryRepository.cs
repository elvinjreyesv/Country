using ERV.App.ConnectedServices.Countries;
using ERV.App.DataAccess.Repositories.Abstracts;
using ERV.App.Infrastructure.Utils;
using ERV.App.Infrastructure.Utils.Constants;
using ERV.App.Models.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.DataAccess.Repositories
{
    public class CountryRepository: ICountryRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly WebApiAppSettings _settings;
        private readonly string ApiUrl;
        private readonly MemoryCacheEntryOptions cacheExpiryOptions;
        public CountryRepository(IOptions<WebApiAppSettings> options, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _settings = options.Value;
            ApiUrl = _settings.ExternalServices?.FirstOrDefault(row => row.Key == ApiKeyConstants.Country_Key)?.BaseUrl ?? string.Empty;

            cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(_settings.CacheConfiguration.AbsoluteExpirationSeconds),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromSeconds(_settings.CacheConfiguration.SlidingExpirationSeconds)
            };
        }
        public IApiMethods Rest => RestService.For<IApiMethods>(ApiUrl);

        public async Task<List<Country>> GetCountries()
        {
            try
            {
                if (!_memoryCache.TryGetValue(CacheKeyConstants.Countries, out List<Country> countries))
                {
                    countries = await Rest.GetCountries();
                    _memoryCache.Set(CacheKeyConstants.Countries, countries, cacheExpiryOptions);
                }

                return countries;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Country>().ToList();
            }
        }
        public async Task<Country> GetCountryDetails(string countryCode)
        {
            try
            {
                var country = new Country();
                var cacheKey = $"{CacheKeyConstants.Country}_{countryCode}";

                if (!_memoryCache.TryGetValue(cacheKey, out country)
                    && !_memoryCache.TryGetValue(cacheKey, out country))
                {
                    country = (await Rest.GetCountryDetails(countryCode))?.FirstOrDefault();
                    _memoryCache.Set(cacheKey, country, cacheExpiryOptions);
                }
                    
                return country;
            }
            catch (Exception ex)
            {
                return default(Country);
            }
        }
        public async Task<List<Country>> GetRegionDetails(string region)
        {
            try
            {
                var cacheKey = $"{CacheKeyConstants.Region}_{region}";
                if (!_memoryCache.TryGetValue(cacheKey, out List<Country> regionDetails))
                {
                    regionDetails = await Rest.GetRegionDetails(region);
                    _memoryCache.Set(cacheKey, regionDetails, cacheExpiryOptions);
                }

                return regionDetails;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Country>().ToList();
            }
        }
        public async Task<List<Country>> GetSubRegions(string subRegionName)
        {
            try
            {
                var cacheKey = $"{CacheKeyConstants.SubRegion}_{subRegionName}";
                if (!_memoryCache.TryGetValue(cacheKey, out List<Country> subRegionDetails))
                {
                    subRegionDetails = await Rest.GetSubRegions(subRegionName);
                    _memoryCache.Set(cacheKey, subRegionDetails, cacheExpiryOptions);
                }
                return subRegionDetails;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Country>().ToList();
            }
        }
    }
}
