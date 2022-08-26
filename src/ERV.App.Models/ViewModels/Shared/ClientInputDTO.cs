using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Models.ViewModels.Shared
{
    public class ClientInputDTO
    {
        [Required, JsonProperty("Token")]
        public string Token { get; set; }

        [Required, JsonProperty("Id")]
        public string Id { get; set; }

        public virtual void SetDefaults()
        {
        }
    }
}
