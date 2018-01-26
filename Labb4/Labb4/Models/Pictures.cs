using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Models
{
    class Pictures
    {
        [JsonProperty("_id")]
        public string _id { get; set; }
        [JsonProperty("PictureName")]
        public string PictureName { get; set; }
        [JsonProperty("PictureUrl")]
        public string PictureUrl { get; set; }
    }
}
