using Labb4.App_Start;
using Labb4.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
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
        Account accounts = new Account();

        public Account CreateAccount(string _Id, string username, string email, List<string> pictureId)
        { 
            try
            {
                //Account document = (from doc in Context.Client.CreateDocumentQuery<Account>(Context.Database.SelfLink)
                //                     where doc._id == _Id
                //                     select doc).FirstOrDefault();

                Account account = Context.Client.CreateDocumentQuery<Account>(Context.Collection.DocumentsLink)
                    .Where(d => d._id == _Id).AsEnumerable().FirstOrDefault();

                if (account == null)
                {
                    Account newAccount = new Account() { };
                    var query = Context.Client.CreateDocumentQuery<Account>(new Uri(Context.Collection.SelfLink), newAccount);
                        
                }
                else
                {
                    return account;
                }
            }
            catch(Exception e)
            {
                return null;
                throw e;
            }
        }

        public IEnumerable<Account> GetAllAccounts()
        {
           

            try
            {
                IEnumerable<Account> account = Context.Client.CreateDocumentQuery<Account>(Context.Collection.DocumentsLink).AsEnumerable();
                return account;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public Account GetAccountById(string _Id)
        {
            try
            {
                Account account = Context.Client.CreateDocumentQuery<Account>(Context.Collection.DocumentsLink)
                    .Where(d => d._id == _Id).AsEnumerable().FirstOrDefault();

                return account;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
            
        }

        public Account UpdateAccountDocument(string _id)
        {

        }

        public Account DeleteAccountDocument(string _id)
        {
            try
            {
                Account account = Context.Client<Account>(Context.Collection.DocumentsLink)
                    .Where(d => d._id == _Id).AsEnumerable().FirstOrDefault();

                var account = client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(Context.Database.SelfLink, Context.Collection.SelfLink, _id);
            }
            catch (Exception e)
            {
                return null;
                throw e;  
            }
        }
    }
}
