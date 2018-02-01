using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using PictureFunction.Models;

namespace PictureFunction
{
    public static class Function1
    {
        [FunctionName("Mode")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            Picture picture = await req.Content.ReadAsAsync<Picture>();

            log.Info($"{picture.Id}, {picture.PictureName}");

            return req.CreateResponse(HttpStatusCode.OK, $"sent:{picture.PictureName}");
        }
    }
}
