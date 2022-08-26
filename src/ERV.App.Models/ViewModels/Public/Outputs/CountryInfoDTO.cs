using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Models.ViewModels.Public.Outputs
{
    public class CountryInfoDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Flag Flag { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        public int Index { get; set; }
    }
    public class Flag
    {
        public string Png { get; set; }
        public string Svg { get; set; }
    }
}
