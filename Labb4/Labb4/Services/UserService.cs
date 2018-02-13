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


        public IEnumerable<Picture> GetAllPicturesByUserName(string userName)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    pictureUser = Client.CreateDocumentQuery<PictureUser>(Context.GetCollectionByName("PictureUsers").SelfLink)
                                            .Where(d => d.Username == userName).AsEnumerable().FirstOrDefault();

                    
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
                return null;
                throw e;
            }
        }

        public Picture AddExistingPictureToPictureUserByUserName(string UserName, string PictureName)
        {
            try
            {
                pictureUser = pictureUserController.GetPictureUserByUserName(UserName);
                picture = pictureController.GetPictureByPictureName(PictureName);
                pictureUser.PictureId.Add(picture.Id);

                PictureUser updatedPictureUser = pictureUserController.UpdatePictureUserDocument(pictureUser.Username, pictureUser.Email, pictureUser.PictureId);

                return picture;
            }
            catch (Exception e)
            {
                return null;
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

                PictureUser updatedPictureUser = pictureUserController.UpdatePictureUserDocument(pictureUser.Username, pictureUser.Email, pictureUser.PictureId);

                return picture;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public Picture AddNewPictureToUser(string userName, string pictureName, string pictureUrl)
        {
            picture = pictureController.CreatePicture(pictureName, pictureUrl);
            AddExistingPictureToPictureUserByUserName(userName, picture.PictureName);

            return picture;
        }

        public Picture UpdatePictureOfUser(string userName, string pictureName, string pictureUrl)
        {
            picture = pictureController.UpdatePictureDocument(pictureName, pictureUrl, false);

            return picture;
        }

        public Picture RemovePictureFromUser(string userName,string pictureName)
        {
            picture = pictureController.UpdatePictureDocument(pictureName, pictureUrl, false);

            return picture;
        }
    }
}
