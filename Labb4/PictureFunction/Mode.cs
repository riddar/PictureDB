using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using PictureFunction.Controllers;
using PictureFunction.Models;

namespace PictureFunction
{
    public static class Mode
    {
        [FunctionName("Mode")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            PictureController pictureController = new PictureController();
            log.Info("C# HTTP trigger function processed a request.");

            string ViewReviewQueue = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "ViewReviewQueue", true) == 0).Value;
            string Approved = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "Approved", true) == 0).Value;
            string Rejected = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "Rejected", true) == 0).Value;

            if (ViewReviewQueue != null)
            {
                List<string> newPictures = new List<string>();
                var pictures = pictureController.GetAllPictures();
                var json = JsonConvert.SerializeObject(pictures);
                foreach (var picture in pictures)
                {
                    if (picture.Valid == false)
                    {
                        newPictures.Add(picture.PictureName);
                    }
                }
                return req.CreateResponse(HttpStatusCode.OK, $"{json}");
            }
            else if (Approved != null)
            {
                var picture = pictureController.GetPictureByPictureName(Approved);
                if (picture != null)
                {
                    if (picture.Valid == true)
                    {
                        return req.CreateResponse(HttpStatusCode.OK, $"{JsonConvert.SerializeObject(picture)}, picture already approved!");
                    }
                    else if (picture.Valid != true)
                    {
                        var updatedPicture = pictureController.UpdatePictureDocument(Approved, true);
                        return req.CreateResponse(HttpStatusCode.OK, $"{JsonConvert.SerializeObject(updatedPicture)}, picture approved!");
                    }
                }
                else
                {
                    return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body");
                }
                
            }
            else if (Rejected != null)
            {
                var picture = pictureController.GetPictureByPictureName(Rejected);
                if (picture != null)
                {
                    if (picture.Valid == false)
                    {
                        return req.CreateResponse(HttpStatusCode.OK, $"{JsonConvert.SerializeObject(picture)}, picture already rejected!");
                    }
                    else if (picture.Valid != false)
                    {
                        var updatedPicture = pictureController.UpdatePictureDocument(Rejected, false);
                        return req.CreateResponse(HttpStatusCode.OK, $"{JsonConvert.SerializeObject(updatedPicture)}, picture rejected!");
                    }
                }
                else
                {
                    return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body");
                }
            }
            else
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body");
            }

            return null;
        }
    }
}
