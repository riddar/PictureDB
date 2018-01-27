using Labb4.App_Start;
using Labb4.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Controllers
{
    public class AccountsController
    {
        MongoContext Context = new MongoContext("Accounts");

        public Account CreateAccount(string _Id, string username, string email, List<string> pictureId)
        { 
            try
            {
                Account account = Context.Client.CreateDocumentQuery<Account>(Context.Collection.SelfLink)
                    .Where(d => d._Id == _Id).AsEnumerable().FirstOrDefault();

                if (account == null)
                {
                    Account newAccount = new Account() { _Id = _Id, Username = username, Email = email, PictureId = pictureId };
                    Context.Client.CreateDocumentAsync(Context.Collection.SelfLink, newAccount);
                    return newAccount;
                        
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

        public Account GetAccountByUsername(string username)
        {
            try
            {
                Account account = Context.Client.CreateDocumentQuery<Account>(Context.Collection.DocumentsLink)
                    .Where(d => d.Username == username).AsEnumerable().FirstOrDefault();

                return account;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
            
        }

        public Document GetDocumentById(string _Id)
        {
            try
            {
                return Context.Client.CreateDocumentQuery(Context.Collection.SelfLink)
                   .Where(D => D.Id == _Id)
                   .AsEnumerable()
                   .First();
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public Account UpdateAccountDocument(string username, string email, List<string> pictureId)
        {
            try
            {
                var account = Context.Client.CreateDocumentQuery<Account>(Context.Collection.SelfLink)
                    .Where(d => d.Username == username).AsEnumerable().FirstOrDefault();

                if (account != null)
                {
                    account.Username = username;
                    account.Email = email;
                    account.PictureId = pictureId;
                    Context.Client.ReplaceDocumentAsync(Context.Collection.SelfLink, account);
                    return account;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public void DeleteAccountDocumentByUsername(string username)
        {
            try
            {
                var account = Context.Client.CreateDocumentQuery<Account>(Context.Collection.SelfLink)
                    .Where(d => d.Username == username).AsEnumerable().FirstOrDefault();

                Document document = GetDocumentById(account._Id);

                if (account != null)
                {
                    Context.Client.DeleteDocumentAsync(document.SelfLink);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
