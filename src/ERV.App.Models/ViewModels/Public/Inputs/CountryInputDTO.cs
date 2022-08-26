using ERV.App.Models.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERV.App.Models.ViewModels.Public.Inputs
{
    public class CountryInputDTO : ClientInputDTO
    {
        public string Code { get; set; }

        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }
}
