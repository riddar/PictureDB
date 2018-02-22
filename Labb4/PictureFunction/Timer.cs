using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using PictureFunction.Controllers;

namespace PictureFunction
{
    public static class Timer
    {
        [FunctionName("Timer")]
        public static void Run([TimerTrigger("0 */20 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
            PictureController pictureController = new PictureController();

            var pictures = pictureController.GetAllPictures();

            foreach (var picture in pictures)
            {
                if (picture.Valid == false && picture.PictureName.EndsWith(".png"))
                {
                    pictureController.UpdatePictureByPictureName(picture.PictureName, true);
                }
            }
        }
    }
}
