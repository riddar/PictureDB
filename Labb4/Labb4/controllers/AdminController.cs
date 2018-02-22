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
    class AdminController
    {
        CosmosDBContext Context = new CosmosDBContext();
        Admin _Admin = new Admin();

        public Admin CreateAdmin(string id, string adminName, string password)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    _Admin = GetAdminByAdminName(adminName);

                    if (_Admin == null)
                    {
                        Admin newAdmin = new Admin() { Id = id, AdminName = adminName, Password = password };
                        Client.CreateDocumentAsync(Context.GetCollectionByName("Pictures").SelfLink, newAdmin);
                        return newAdmin;
                    }
                    else
                    {
                        return _Admin;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Admin> GetAllAdmin()
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    IEnumerable<Admin> admins = Client.CreateDocumentQuery<Admin>(Context.GetCollectionByName("Admins").SelfLink).AsEnumerable();
                    return admins;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Admin GetAdminByAdminName(string adminName)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    _Admin = Client.CreateDocumentQuery<Admin>(Context.GetCollectionByName("Admins").SelfLink)
                        .Where(d => d.AdminName == adminName).AsEnumerable().FirstOrDefault();

                    return _Admin;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Document GetDocumentById(string id)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    return Client.CreateDocumentQuery(Context.GetCollectionByName("Admins").SelfLink)
                       .Where(D => D.Id == id)
                       .AsEnumerable()
                       .First();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public Admin UpdatePasswordForAdminDocument(string adminName, string password)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    _Admin = GetAdminByAdminName(adminName);

                    Document document = GetDocumentById(_Admin.Id);
                    document.SetPropertyValue("Password", password);

                    Client.ReplaceDocumentAsync(document);

                    _Admin = GetAdminByAdminName(adminName);

                    return _Admin;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Admin DeleteAdminByAdminName(string adminName)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    _Admin = GetAdminByAdminName(adminName);
                    Document document = GetDocumentById(_Admin.Id);

                    if (document == null)
                        return null;

                    Client.DeleteDocumentAsync(document.SelfLink);
                    return _Admin;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
