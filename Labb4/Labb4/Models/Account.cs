using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Models
{
    public class Account
    {
        [JsonProperty("_Id")]
        public string _Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("pictureId")]
        public List<string> PictureId { get; set; }
    }
}
