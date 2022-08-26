using ERV.App.DataAccess.Repositories.Abstracts;
using ERV.App.Models.ViewModels.Public.Outputs;
using ERV.App.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERV.App.Infrastructure.Extensions;
using ERV.App.Models.ViewModels.Shared;
using ERV.App.Models.Enums;
using ERV.App.Models.Entities;
using Newtonsoft.Json;
using System.Collections;
using System.Diagnostics.Metrics;

namespace ERV.App.Services
{
    public class CountryService: ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public async Task<List<CountryInfoDTO>> Countries()
        {
            try
            {
                var output = Enumerable.Empty<CountryInfoDTO>().ToList();
                var countries = await _countryRepository.GetCountries();

                if(countries !=null && countries.Any())
                {
                    output = countries.Select(row => new CountryInfoDTO()
                    {
                        Code = row.cca2.CleanSpace(),
                        Name = row.name.common,
                        Flag = new Flag()
                        {
                            Png = row.flags.png,
                            Svg = row.flags.svg
                        },
                        Region = row.region,
                        SubRegion = row.subregion
                    }).OrderBy(row=>row.Name).ToList();
                }

                return output;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<CountryInfoDTO>().ToList();
            }
        }
        public async Task<AppResponse<EAppResponse, CountryDTO>> CountryInformation(string countryCode)
        {
            var output = default(CountryDTO);

            if (string.IsNullOrWhiteSpace(countryCode))
                return AppResponse.Create(EAppResponse.InvalidInput, output);

            try
            {
                var country = await _countryRepository.GetCountryDetails(countryCode);
                var countryList = Enumerable.Empty<Country>().ToList();

                if (country != null)
                {
                    if (country.borders != null && country.borders.Any())
                        countryList = await _countryRepository.GetCountries();

                    output = (await MapCountriesContent(new List<Country> { country }, countryList)).FirstOrDefault();
                }
 
                return AppResponse.Create(EAppResponse.Success, output);
            }
            catch (Exception ex)
            {
                //_logSystemService.AddExceptionLog(ex);
                return AppResponse.Create(EAppResponse.UnhandledError, output);
            }
        }
        public async Task<AppResponse<EAppResponse, RegionDTO>> RegionInformation(string regionName)
        {
            var output = default(RegionDTO);

            if (string.IsNullOrWhiteSpace(regionName))
                return AppResponse.Create(EAppResponse.InvalidInput, output);

            try
            {
                var regionDetails = await _countryRepository.GetRegionDetails(regionName);
                if (regionDetails != null && regionDetails.Any())
                {
                    output = new RegionDTO()
                    {
                        Name = regionName,
                        SubRegions = await MapSubRegionsContent(regionDetails),
                    };
                }

                return AppResponse.Create(EAppResponse.Success, output);
            }
            catch (Exception ex)
            {
                //_logSystemService.AddExceptionLog(ex);
                return AppResponse.Create(EAppResponse.UnhandledError, output);
            }
        }
        public async Task<AppResponse<EAppResponse, SubRegionDTO>> SubRegionInformation(string subRegionName)
        {
            var output = default(SubRegionDTO);

            if (string.IsNullOrWhiteSpace(subRegionName))
                return AppResponse.Create(EAppResponse.InvalidInput, output);

            try
            {
                var subRegionDetails = await _countryRepository.GetRegionDetails(subRegionName);
                if (subRegionDetails != null && subRegionDetails.Any())
                {
                    var countryList = await _countryRepository.GetCountries();
                    output = new SubRegionDTO()
                    {
                        Name = subRegionName,
                        Countries = await MapCountriesContent(subRegionDetails, countryList),
                        RegionName = subRegionDetails?.FirstOrDefault(row=>row !=null)?.region ?? string.Empty
                    };
                }

                return AppResponse.Create(EAppResponse.Success, output);
            }
            catch (Exception ex)
            {
                //_logSystemService.AddExceptionLog(ex);
                return AppResponse.Create(EAppResponse.UnhandledError, output);
            }
        }

        #region Helper
        private async Task<List<CountryDTO>> MapCountriesContent(List<Country> countries, List<Country> countriesBorder)
        {
            var output = Enumerable.Empty<CountryDTO>().ToList();
            var borders = Enumerable.Empty<CountryInfoDTO>().ToList();

            foreach(var country in countries)
            {
                if(country.borders !=null && country.borders.Any() 
                    && countriesBorder !=null && countriesBorder.Any())
                    borders = (countriesBorder.Where(row => country.borders.Contains(row.cca3))
                    .Select(row => new CountryInfoDTO()
                    {
                        Code = row.cca2.CleanSpace(),
                        Name = row.name.common,
                        Flag = new Flag()
                        {
                            Png = row.flags.png,
                            Svg = row.flags.svg
                        }
                    }).ToList()) ?? Enumerable.Empty<CountryInfoDTO>().ToList();

                output.Add(new CountryDTO()
                {
                    Code = country.cca2.CleanSpace(),
                    Name = country.name.common,
                    CapitalCities = country.capital,
                    Population = country.population,
                    BorderCountries = borders,
                    Flag = new Flag()
                    {
                        Png = country.flags.png,
                        Svg = country.flags.svg
                    },
                    Region = country.region,
                    SubRegion = country.subregion,
                    Currencies = MapCurrencies(country.currencies),
                    Languages = MapLanguages(country.languages)
                });
            }
           
            return output.OrderBy(row => row.Name).ToList();
        }
        private async Task<List<SubRegionDTO>> MapSubRegionsContent(List<Country> countries)
        {
            var output = Enumerable.Empty<SubRegionDTO>().ToList();
            var subRegions = countries.GroupBy(row => row.subregion).ToList();
            var countryList = await _countryRepository.GetCountries();

            foreach (var item in subRegions)
            {
                output.Add(new SubRegionDTO()
                {
                    Name = item.Key,
                    RegionName = item.FirstOrDefault()?.region ?? string.Empty,
                    Countries = await MapCountriesContent(item.ToList(), countryList)
                });
            }

            return output.OrderBy(row => row.Name).ToList();
        }
        private List<LanguageDTO> MapLanguages(Hashtable lang)
        {
            var languages = Enumerable.Empty<LanguageDTO>().ToList();
            if (lang != null)
            {
                foreach (DictionaryEntry item in lang)
                    languages.Add(new LanguageDTO()
                    {
                        Code = item.Key.ToString() ?? string.Empty,
                        Name = item.Value.ToString() ?? string.Empty
                    });

            }
            return languages;
        }
        private List<CurrencyDTO> MapCurrencies(Dictionary<string, Currency> currencies)
        {
            var output = Enumerable.Empty<CurrencyDTO>().ToList();

            if (currencies != null)
                foreach (var item in currencies)
                    output.Add(new CurrencyDTO()
                    {
                        Code = item.Key,
                        Name = item.Value.name,
                        Symbol = item.Value.symbol
                    });

            return output;
        }
        #endregion
    }
}
