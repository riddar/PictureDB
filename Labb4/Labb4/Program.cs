using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb4.Controllers;

namespace Labb4
{
    class Program
    {
        AccountsController accountsController = new AccountsController();


        static void Main(string[] args)
        {
            var accounts = accountsController().GetAllAccounts();
        }
    }
}
