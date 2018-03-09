using System;
using System.Text.RegularExpressions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using PictureFunction.Controllers;

namespace PictureFunction
{
    public static class Timer
    {
        [FunctionName("Timer")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            PictureController pictureController = new PictureController();
            var pictures = pictureController.GetAllPictures();

            foreach (var picture in pictures)
            {
                if (picture.Valid == false && Regex.IsMatch(picture.PictureName, @"\.png$"))
                {
                    pictureController.UpdatePictureByPictureName(picture.PictureName, true);
                }
            }

            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
