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
        AllowedPicturesControllers allowedPictures = new AllowedPicturesControllers();
        Pictures pictures = new Pictures();

        public IEnumerable<Document> GetAccountDocuments()
        {
            MongoContext context = new MongoContext("Accounts");

            try
            {
                var Documents = (from doc in context.Client.CreateDocumentQuery(context.Collection.SelfLink) select doc).AsEnumerable();
                return Documents;
                
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
