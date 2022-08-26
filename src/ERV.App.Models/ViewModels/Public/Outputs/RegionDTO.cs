using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Models.ViewModels.Public.Outputs
{
    public class RegionDTO
    {
        public string Name { get; set; }
        public List<SubRegionDTO> SubRegions { get; set; }
        public int TotalPopulation => SubRegions.Sum(row => row.TotalPopulation);
    }
}
