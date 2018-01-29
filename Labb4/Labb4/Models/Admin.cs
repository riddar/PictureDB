using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Models
{
    class Admin
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("AdminName")]
        public string AdminName { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}
