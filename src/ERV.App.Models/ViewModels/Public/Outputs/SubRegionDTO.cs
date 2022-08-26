using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Models.ViewModels.Public.Outputs
{
    public class SubRegionDTO
    {
        public string Name { get; set; }
        public string RegionName { get; set; }
        public List<CountryDTO> Countries { get; set; }
        public int TotalPopulation => Countries.Sum(row => row.Population);
    }
}
