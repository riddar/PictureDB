using Labb4.Controllers;
using Labb4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Services
{
    class AdminService
    {
        AdminController adminController = new AdminController();
        PictureUserController pictureUserController = new PictureUserController();
        PictureController PictureController = new PictureController();

        List<PictureUser> pictureUsers = new List<PictureUser>();
        PictureUser pictureUser = new PictureUser();
        List<Picture> pictures = new List<Picture>();
        Picture picture = new Picture();
        Admin admin = new Admin();

        public bool CheckPassword(string adminName, string password)
        {
            admin = adminController.GetAdminByAdminName(adminName);
            if (admin.Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<PictureUser> GetAllUser()
        {
            pictureUsers = pictureUserController.GetAllPictureUser().ToList();
            return pictureUsers;
        }

        public PictureUser CreateUser(string userName, string email, List<string> pictureId)
        {
            pictureUser = pictureUserController.CreatePictureUser(userName, email, pictureId);
            return pictureUser;
        }

        public PictureUser RemoveUser(string userName)
        {
            pictureUser = pictureUserController.DeletePictureUserByUserName(userName);
            return pictureUser;
        }

        public Picture UpdatePicture(string pictureName, string pictureUrl, bool valid)
        {
            picture = PictureController.UpdatePictureByPictureName(pictureName, pictureUrl, valid);
            return picture;
        }

        public List<Picture> GetAllPictures()
        {
            pictures = PictureController.GetAllPictures().ToList();
            return pictures;
        }

        public List<Picture> GetAllFalsePictures()
        {
            pictures = PictureController.GetAllPictures().ToList();
            List<Picture> newPictures = new List<Picture>();
            foreach (var picture in pictures)
            {
                if (picture.Valid == false)
                {
                    newPictures.Add(picture);
                }
            }
            return newPictures;
        }

        
    }
}
