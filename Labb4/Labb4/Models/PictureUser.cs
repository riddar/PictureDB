using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Models
{
    public class PictureUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("Username")]
        public string Username { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
        [JsonProperty("PictureId")]
        public List<string> PictureId { get; set; }
    }
}
