using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using PictureFunction.Models;

namespace PictureFunction
{
    public static class Mode
    {
        [FunctionName("Mode")]
        public static void Run([QueueTrigger("ViewReviewQueue", Connection = "")]Picture picture,
            [Blob("Approved/{rand-guid}")] out string Accepted,
            [Blob("Rejected/{rand-guid}")] out string Rejected,

            TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {picture.Id}");

            bool isRejected = picture.Valid == false;

            if (isRejected)
            {
                Rejected = JsonConvert.SerializeObject(picture);
                Accepted = null;
            }
            else
            {
                Accepted = JsonConvert.SerializeObject(picture);
                Rejected = null;
            }
        }
    }
}
