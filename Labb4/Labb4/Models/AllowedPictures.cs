using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Models
{
    class AllowedPictures
    {
        [JsonProperty("_id")]
        public string _id { get; set; }
        [JsonProperty("PicturesId")]
        public string PicturesId { get; set; }
        [JsonProperty("Valid")]
        public bool Valid { get; set; }
    }
}
