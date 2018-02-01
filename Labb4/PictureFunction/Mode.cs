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
    public static class Mode
    {
        [FunctionName("Mode")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string ViewReviewQueue = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "ViewReviewQueue", true) == 0).Value;
            string Approved = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "Approved", true) == 0).Value;
            string Rejected = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "Rejected", true) == 0).Value;

            // Set name to query string or body data
            if (ViewReviewQueue != null)
            {
                return req.CreateResponse(HttpStatusCode.OK, $"{ViewReviewQueue}");
            }
            else if (Approved != null)
            {
                return req.CreateResponse(HttpStatusCode.OK, $"{Approved}");
            }
            else if (Rejected != null)
            {
                return req.CreateResponse(HttpStatusCode.OK, $"{Rejected}");
            }
            else
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body");
            }
        }
    }
}
