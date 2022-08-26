using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Models.Entities
{
    public class Country
    {
        public Name name { get; set; }
        public string cca2 { get; set; }
        public string cca3 { get; set; }
        public Dictionary<string, Currency> currencies { get; set; }
        public List<string> capital { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        public Hashtable languages { get; set; }
        public List<string> borders { get; set; }
        public string flag { get; set; }
        public int population { get; set; }
        public Flags flags { get; set; }
        public CapitalInfo capitalInfo { get; set; }
    }
    public class CapitalInfo
    {
        public List<double> latlng { get; set; }
    }

    public class Name
    {
        public string common { get; set; }
        public string official { get; set; }
    }

    public class Flags
    {
        public string png { get; set; }
        public string svg { get; set; }
    }
}
