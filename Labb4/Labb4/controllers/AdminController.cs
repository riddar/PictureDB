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
        Admin admin = new Admin();

        public Admin CreateAdmin(string id, string adminName, string password)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    admin = Client.CreateDocumentQuery<Admin>(Context.GetCollectionByName("Admin").SelfLink)
                        .Where(d => d.Id == id).AsEnumerable().FirstOrDefault();
                    if (admin == null)
                    {
                        Admin newAdmin = new Admin() { Id = id, AdminName = adminName, Password = password };
                        Client.CreateDocumentAsync(Context.GetCollectionByName("Pictures").SelfLink, newAdmin);
                        return newAdmin;
                    }
                    else
                    {
                        return admin;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public IEnumerable<Admin> GetAllAdmin()
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    List<Admin> list = new List<Admin>();
                    IEnumerable<Admin> admins = Client.CreateDocumentQuery<Admin>(Context.GetCollectionByName("Admins").SelfLink).AsEnumerable();
                    foreach (var admin in admins)
                    {
                        list.Add(admin);
                    }
                    return list;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public Admin GetAdminByAdminName(string adminName)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    admin = Client.CreateDocumentQuery<Admin>(Context.GetCollectionByName("Admins").SelfLink)
                        .Where(d => d.AdminName == adminName).AsEnumerable().FirstOrDefault();

                    return admin;
                }
            }
            catch (Exception e)
            {
                return null;
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

        public Admin UpdateAdminDocument(string adminName, string password)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    admin = Client.CreateDocumentQuery<Admin>(Context.GetCollectionByName("Admins").SelfLink)
                        .Where(d => d.AdminName == adminName).AsEnumerable().FirstOrDefault();

                    if (admin != null)
                    {
                        admin.AdminName = adminName;
                        admin.Password = password;
                        Client.ReplaceDocumentAsync(Context.GetCollectionByName("Pictures").SelfLink, admin);
                        return admin;
                    }
                    else
                    {
                        return null;
                    }
                }


            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public void DeleteAdminByAdminName(string adminName)
        {

            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    admin = Client.CreateDocumentQuery<Admin>(Context.GetCollectionByName("Admins").SelfLink)
                    .Where(d => d.AdminName == adminName).AsEnumerable().FirstOrDefault();

                    Document document = GetDocumentById(admin.Id);

                    if (admin != null)
                    {
                        Client.DeleteDocumentAsync(document.SelfLink);
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
