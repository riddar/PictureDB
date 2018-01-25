using Labb4.App_Start;
using Labb4.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Controllers
{
    public class AccountsController
    {
        Accounts accounts = new Accounts();
        AllowedPictures allowedPictures = new AllowedPictures();
        Pictures pictures = new Pictures();

        public IEnumerable<Document> GetAllAccounts()
        {

            try
            {
                return new MongoContext("Accounts").Documents.AsEnumerable();
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
