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

        public Document CreateOrUpdateDocumentById(string _Id, string username, string email, List<string> pictureId)
        {
            Document newDocument = new Document { Id = _Id, new username, new email, new pictureId };

            

            Document document = null;
            try
            {
                document = (from doc in Context.Client.CreateDocumentQuery(Context.Database.SelfLink)
                                where doc.Id == _Id
                                select doc).FirstOrDefault();
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                if (document == null)
                {
                    Context.Collection..Add(score);
                }
                else
                {
                    return document;
                }
            }
        }

        public IEnumerable<Document> GetAllAccountsDocuments()
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

        public Document GetAccountDocumentById(string _id)
        {
            try
            {
                var document = (from doc in Context.Client.CreateDocumentQuery(Context.Database.SelfLink)
                                where doc.Id == _id
                                select doc).FirstOrDefault();
                return document;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
            
        }

        public Document UpdateAccountDocument(string _id)
        {

        }

        public Document DeleteAccountDocument(string _id)
        {

        }
    }
}
