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
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "PictureName")]
        public string PictureName { get; set; }
        [JsonProperty(PropertyName = "PictureUrl")]
        public string PictureUrl { get; set; }
        [JsonProperty(PropertyName = "Valid")]
        public bool Valid { get; set; }
    }
}
