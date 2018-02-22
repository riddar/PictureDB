using Labb4.App_Start;
using Labb4.Controllers;
using Labb4.Models;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Services
{
    public class UserService
    {
        CosmosDBContext Context = new CosmosDBContext();
        PictureController pictureController = new PictureController();
        PictureUserController pictureUserController = new PictureUserController();
        PictureUser pictureUser = new PictureUser();
        Picture picture = new Picture();

        public PictureUser CheckpictureUser(string userName)
        {
            pictureUser = pictureUserController.GetPictureUserByUserName(userName);
            return pictureUser;
        }

        public IEnumerable<Picture> GetAllPicturesByUserName(string userName)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    pictureUser = pictureUserController.GetPictureUserByUserName(userName);
                    
                    List<Picture> pictures = new List<Picture>();
                    foreach (var pictureId in pictureUser.PictureId)
                    {
                        pictures.Add(pictureController.GetPictureById(pictureId));
                    }


                    return pictures;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Picture AddExistingPictureToPictureUserByUserName(string UserName, string PictureName)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    pictureUser = pictureUserController.GetPictureUserByUserName(UserName);
                    picture = pictureController.GetPictureByPictureName(PictureName);
                    pictureUser.PictureId.Add(picture.Id);

                    PictureUser updatedPictureUser = pictureUserController.UpdatePictureUserByUsername(pictureUser.Username, pictureUser.Email, pictureUser.PictureId);
                }

                return picture;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Picture RemoveExistingPictureToPictureUserByUserName(string UserName, string PictureName)
        {
            try
            {
                pictureUser = pictureUserController.GetPictureUserByUserName(UserName);
                picture = pictureController.GetPictureByPictureName(PictureName);
                pictureUser.PictureId.Remove(picture.Id);

                PictureUser updatedPictureUser = pictureUserController.UpdatePictureUserByUsername(pictureUser.Username, pictureUser.Email, pictureUser.PictureId);

                return picture;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Picture CreateNewPictureToUser(string userName, string pictureName, string pictureUrl)
        {
            picture = pictureController.CreatePicture(pictureName, pictureUrl);
            return picture;
        }

        public Picture UpdatePictureOfUser(string userName, string pictureName, string pictureUrl)
        {
            bool valid = false;
            picture = pictureController.UpdatePictureByPictureName(pictureName, pictureUrl, valid);

            return picture;
        }

        public Picture RemovePictureFromUser(string userName,string pictureName)
        {
            picture = pictureController.UpdatePictureByPictureName(pictureName, pictureController.GetPictureByPictureName(pictureName).PictureUrl, false);

            return picture;
        }
    }
}
