using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Models
{
    public class Picture
    {
        [JsonProperty("_Id")]
        public string _Id { get; set; }
        [JsonProperty("PictureName")]
        public string PictureName { get; set; }
        [JsonProperty("PictureUrl")]
        public string PictureUrl { get; set; }
        [JsonProperty("Valid")]
        public bool Valid { get; set; }
    }
}
