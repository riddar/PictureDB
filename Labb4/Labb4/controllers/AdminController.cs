using Labb4.App_Start;
using Labb4.Models;
using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Controllers
{
    class AdminController
    {
        MongoContext Context = new MongoContext("Admin");

        public Admin CreateAdmin(string _id, string adminName, string password)
        {
            try
            {
                Admin admin = Context.Client.CreateDocumentQuery<Admin>(Context.Collection.SelfLink)
                    .Where(d => d._Id == _id).AsEnumerable().FirstOrDefault();

                if (admin == null)
                {
                    Admin newAdmin = new Admin() { _Id = _id, AdminName = adminName, Password = password};
                    Context.Client.CreateDocumentAsync(Context.Collection.SelfLink, newAdmin);
                    return newAdmin;

                }
                else
                {
                    return admin;
                }
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public IEnumerable<Admin> GetAllAdmins()
        {
            try
            {
                IEnumerable<Admin> admin = Context.Client.CreateDocumentQuery<Admin>(Context.Collection.DocumentsLink).AsEnumerable();
                return admin;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Admin GetAdmin(string adminName)
        {
            try
            {
                Admin admin = Context.Client.CreateDocumentQuery<Admin>(Context.Collection.DocumentsLink)
                    .Where(d => d.AdminName == adminName).AsEnumerable().FirstOrDefault();

                return admin;
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

        public Admin UpdateAdminDocument(string adminName, string password)
        {
            try
            {
                var admin = Context.Client.CreateDocumentQuery<Admin>(Context.Collection.SelfLink)
                    .Where(d => d.AdminName == adminName).AsEnumerable().FirstOrDefault();

                if (admin != null)
                {
                    admin.AdminName = adminName;
                    admin.Password = password;
                    Context.Client.ReplaceDocumentAsync(Context.Collection.SelfLink, admin);
                    return admin;
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

        public void DeleteAdminByAdminName(string adminName)
        {
            try
            {
                var admin = Context.Client.CreateDocumentQuery<Admin>(Context.Collection.SelfLink)
                    .Where(d => d.AdminName == adminName).AsEnumerable().FirstOrDefault();

                Document document = GetDocumentById(admin._Id);

                if (admin != null)
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
