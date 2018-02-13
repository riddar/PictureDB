using Labb4.Controllers;
using Labb4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Views
{
    public class AdminView
    {
        AdminService service = new AdminService();

        public AdminView()
        {
            var AdminName = Menu();
            Admins(AdminName);
        }

        public void Header()
        {
            Console.WriteLine("PictureDB");
        }

        public void Footer()
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------");
        }

        public string Menu()
        {
            Console.Clear();
            Header();
            Console.WriteLine("---------------------------");
            Console.WriteLine("  Enter your name below    ");
            Console.WriteLine("---------------------------");
            string name = Console.ReadLine();
            Console.WriteLine("---------------------------");
            Console.WriteLine(" Enter your password below ");
            Console.WriteLine("---------------------------");
            string password = Console.ReadLine();

            if(service.CheckPassword(name, password))
            {
                return name;
            }
            else
            {
                Menu();
            }

            return null;
        }

        public void Admins(string name)
        {
            Console.Clear();
            Header();
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"      Admin: {name}               |");
            Console.WriteLine("|     1.Print all users            |");
            Console.WriteLine("|     2.print all Picture          |");
            Console.WriteLine("|     3.print Rejected Pictures    |");
            Console.WriteLine("|     4.Update Picture             |");
            Console.WriteLine("|     5.Remove User                |");
            Console.WriteLine("|     6.Create User                |");
            Console.WriteLine("|     7.return                     |");
            Console.WriteLine("------------------------------------");
            Footer();

            try
            {
                bool KeepGoing = true;
                do
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            PrintAllUsers();
                            break;
                        case ConsoleKey.D2:
                            PrintAllPictures();
                            break;
                        case ConsoleKey.D3:
                            PrintAllfalsePictures();
                            break;
                        case ConsoleKey.D4:
                            PrintUpdatePicture();
                            break;
                        case ConsoleKey.D5:
                            PrintRemoveUser();
                            break;
                        case ConsoleKey.D6:
                            PrintCreateUser();
                            break;
                        case ConsoleKey.D7:
                            KeepGoing = false;
                            break;
                        default:

                            break;
                    }
                } while (KeepGoing != false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void PrintAllUsers()
        {
            try
            {
                var pictureUsers = service.GetAllUser();
                PictureController Picture = new PictureController();
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
            catch (Exception e)
            {
                throw e;
            }
           
        }

        private void PrintCreateUser()
        {
            try
            {
                Console.WriteLine("what is the username you want to create?");
                Console.Write("Username: ");
                var username = Console.ReadLine();
                Console.WriteLine("What is the email of the user you would like to create?");
                var email = Console.ReadLine();
                List<string> pictureIds = new List<string>();
                bool moveOn = false;
                do
                {
                    Console.WriteLine("Which picture id would you like to add? (empty field will move on)");
                    var pictureId = Console.ReadLine();
                    if (pictureId != "")
                    {
                        pictureIds.Add(pictureId);
                    }
                    else
                    {
                        moveOn = true;
                    }

                }
                while (moveOn != false);

                service.CreateUser(username, email, pictureIds);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        private void PrintRemoveUser()
        {
            Console.WriteLine("what is the username you want to remove?");
            Console.Write("Username: ");
            var username = Console.ReadLine();
            service.RemoveUser(username);
        }

        private void PrintUpdatePicture()
        {
            try
            {
                Console.WriteLine("what is the picture name you want to update?");
                Console.Write("Username: ");
                var pictureName = Console.ReadLine();
                Console.WriteLine("What is the picture link you would like to update?");
                Console.Write("PictureUrl: ");
                var pictureUrl = Console.ReadLine();
                Console.WriteLine("Would you like to validate this picture?");
                Console.Write("Valid: ");
                var Valid = Console.ReadLine();
                if (Valid.ToLower().Equals("yes"))
                {
                    var picture = service.UpdatePicture(pictureName, pictureUrl, true);
                    if (picture != null)
                    {
                        Console.WriteLine("picture updated!");
                    }
                    else
                    {
                        Console.WriteLine("picture not found!");
                    }
                }
                else if (Valid.ToLower().Equals("no"))
                {
                    var picture = service.UpdatePicture(pictureName, pictureUrl, false);
                    if (picture != null)
                    {
                        Console.WriteLine("picture updated!");
                    }
                    else
                    {
                        Console.WriteLine("picture not found!");
                    }
                }
                else
                {
                    Console.WriteLine("Please write yes/no!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void PrintAllfalsePictures()
        {
            try
            {
                var pictures = service.GetAllFalsePictures();
                if (pictures != null)
                {
                    foreach (var picture in pictures)
                    {
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine($"Id:          {picture.Id}");
                        Console.WriteLine($"PictureName: {picture.PictureName}");
                        Console.WriteLine($"PictureUrl:  {picture.PictureUrl}");
                        Console.WriteLine($"Allowed:     {picture.Valid}");
                    }
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                throw e;
            } 
        }

        private void PrintAllPictures()
        {
            try
            {
                var pictures = service.GetAllPictures();
                if (pictures != null)
                {
                    foreach (var picture in pictures)
                    {
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine($"Id:          {picture.Id}");
                        Console.WriteLine($"PictureName: {picture.PictureName}");
                        Console.WriteLine($"PictureUrl:  {picture.PictureUrl}");
                        Console.WriteLine($"Allowed:     {picture.Valid}");
                    }
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                throw e;
            }   
        }
    }
}
