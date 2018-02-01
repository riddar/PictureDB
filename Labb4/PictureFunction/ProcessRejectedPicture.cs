using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using PictureFunction.Models;

namespace PictureFunction
{
    public static class ProcessRejectedPicture
    {
        [FunctionName("Rejected")]
        public static void Run([BlobTrigger("Rejected/{name}", Connection = "")]string JsonObject, 
            string name, 
            TraceWriter log)
        {

            Picture picture = JsonConvert.DeserializeObject<Picture>(JsonObject);

            log.Info($"C# Blob trigger function Processed blob\n Name:{picture.Id} \n Size: {picture.PictureName} Bytes");
        }
    }
}
