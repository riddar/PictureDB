using Labb4.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Views
{
    public class MainView
    {
        private PictureControllers Picture = new PictureControllers();
        AdminController Administator = new AdminController();
        private PictureUserController PictureUser = new PictureUserController();
        
        public MainView()
        {




        }

        public void PrintAllUsers()
        {
            var pictureUsers = PictureUser.GetAllPictureUser();
            foreach (var pictureUser in pictureUsers)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine(pictureUser.Id);
                Console.WriteLine(pictureUser.Username);
                Console.WriteLine(pictureUser.Email);
                foreach (var pictureid in pictureUser.PictureId)
                {
                    var picture = Picture.GetPictureById(pictureid);
                    Console.WriteLine(picture.PictureName);
                }
            }
            Console.ReadKey();
        }

        public void PrintAllAdmins()
        {
            var admins = Administator.GetAllAdmin();
            foreach (var admin in admins)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine(admin.Id);
                Console.WriteLine(admin.AdminName);
                Console.WriteLine(admin.Password);
            }
            Console.ReadKey();

        }

        public void PrintAllPictures()
        {
            var pictures = Picture.GetAllPictures();
            foreach (var picture in pictures)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine(picture.Id);
                Console.WriteLine(picture.PictureName);
                Console.WriteLine(picture.PictureUrl);
                Console.WriteLine(picture.Valid);
            }
            Console.ReadKey();
        }

        public void Users()
        {

        }

        public void Admin()
        {

        }
    }
}
