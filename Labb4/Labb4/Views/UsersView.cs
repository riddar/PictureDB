using Labb4.Controllers;
using Labb4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Views
{
    public class UsersView
    {
        UserService service = new UserService();

        public UsersView()
        {
            var username = Name();
            Users(username);
        }

        public void Header()
        {
            Console.WriteLine("PictureDB");
        }

        public void Footer()
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------");
        }

        public string Name()
        {
            Console.Clear();
            Header();
            Console.WriteLine("---------------------------");
            Console.WriteLine("  Enter your name below    ");
            Console.WriteLine("---------------------------");
            string result = Console.ReadLine();
            return result;
        }

        public void Users(string name)
        {
            Console.Clear();
            Header();
            Console.WriteLine("---------------------------");
            Console.WriteLine($"      {name}             |");
            Console.WriteLine("|     1.Create Picture    |");
            Console.WriteLine("|     2.Add Picture       |");
            Console.WriteLine("|     3.Update Picture    |");
            Console.WriteLine("|     4.Remove Picture    |");
            Console.WriteLine("|     5.Show pictures     |");
            Console.WriteLine("|     6.return            |");
            Console.WriteLine("---------------------------");
            Footer();

            try
            {
                bool KeepGoing = true;
                do
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            PrintCreatePicture(name);
                            break;
                        case ConsoleKey.D2:
                            PrintAddPicture(name);
                            break;
                        case ConsoleKey.D3:
                            PrintUpdatePicture(name);
                            break;
                        case ConsoleKey.D4:
                            //printRemovePicture(name);
                            break;
                        case ConsoleKey.D5:
                            PrintAllPictures(name);
                            break;
                        case ConsoleKey.D6:
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

        public void PrintCreatePicture(string user)
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("what name would you like to give the picture?");
            Console.Write("PictureName: ");
            var pictureName = Console.ReadLine();
            Console.WriteLine("what is the Picture link you want to add?");
            Console.Write("PictureUrl: ");
            var pictureUrl = Console.ReadLine();
            var valid = service.AddNewPictureToUser(user, pictureName, pictureUrl);
            if (valid != null)
                Console.WriteLine($"{valid.PictureName} Created!");
            else
                Console.WriteLine($"{valid.PictureName} already Created.");
            Console.ReadKey();
        }

        public void PrintAddPicture(string user)
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Which picture would you like to add to the list?");
            Console.Write("picture name: ");
            var pictureName = Console.ReadLine();

            var valid = service.AddExistingPictureToPictureUserByUserName(user, pictureName);

            if (valid != null)
                Console.WriteLine($"{valid.PictureName} Added!");
            else
                Console.WriteLine($"{valid.PictureName} already Added.");
            Console.ReadKey();
        }

        public void PrintUpdatePicture(string user)
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("What is the name of the picture you would like to update?");
            Console.Write("PictureName: ");
            var pictureName = Console.ReadLine();
            Console.WriteLine("what is the Picture link you want to update?");
            Console.Write("PictureUrl: ");
            var pictureUrl = Console.ReadLine();
            service.UpdatePictureOfUser(user, pictureName, pictureUrl);
        }

        private void PrintRemovePicture(string user)
        {
            //not done
            throw new NotImplementedException();
        }

        public void PrintAllPictures(string user)
        {
            var pictures = service.GetAllPicturesByUserName(user);
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
}
