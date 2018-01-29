using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Views
{
    class MainView
    {
        public MainView()
        {
            Menu();
        }

        public void Header()
        {
            Console.WriteLine("PictureDB");
        }

        public void Footer()
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------");
        }

        public void Menu()
        {
            Header();
            Console.WriteLine("---------------------------");
            Console.WriteLine($"     Main Menu           |");
            Console.WriteLine("|     1.User              |");
            Console.WriteLine("|     2.Admin             |");
            Console.WriteLine("|     3.Quit              |");
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
                            UsersView usersView = new UsersView();
                            break;
                        case ConsoleKey.D2:
                            AdminView adminView = new AdminView();
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
    }
}
