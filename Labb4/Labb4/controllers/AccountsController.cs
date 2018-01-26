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
        MongoContext Context = new MongoContext("Accounts");
        Accounts accounts = new Accounts();

        public IEnumerable<Document> GetAllAccountDocuments()
        {
           

            try
            {
                var Documents = (from doc in Context.Client.CreateDocumentQuery(Context.Collection.SelfLink) select doc).AsEnumerable();
                return Documents;
                
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public Document GetAccountDocument()
        {
            var document = (from doc in Ac)
        }
    }
}
