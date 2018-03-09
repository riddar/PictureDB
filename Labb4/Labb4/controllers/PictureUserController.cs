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
    public class PictureUserController
    {
        CosmosDBContext Context = new CosmosDBContext();
        PictureUser _PictureUser = new PictureUser();

        public PictureUser CreatePictureUser(string userName, string email, List<string> pictureId)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    _PictureUser = GetPictureUserByUserName(userName);

                    if (_PictureUser == null)
                    {
                        PictureUser newPictureUser = new PictureUser() { Username = userName, Email = email, PictureId = pictureId };
                        Client.CreateDocumentAsync(Context.GetCollectionByName("PictureUsers").SelfLink, newPictureUser);
                        _PictureUser = GetPictureUserByUserName(userName);
                        return newPictureUser;
                    }
                    else
                    {
                        return _PictureUser;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<PictureUser> GetAllPictureUser()
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    IEnumerable<PictureUser> pictureUsers = Client.CreateDocumentQuery<PictureUser>(Context.GetCollectionByName("PictureUsers").SelfLink).AsEnumerable();

                    List<PictureUser> list = new List<PictureUser>();

                    if (pictureUsers == null)
                        return null;

                    foreach (var pictureUser in pictureUsers)
                        list.Add(pictureUser);
                    return list;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PictureUser GetPictureUserByUserName(string userName)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    _PictureUser = Client.CreateDocumentQuery<PictureUser>(Context.GetCollectionByName("PictureUsers").SelfLink)
                                            .Where(d => d.Username == userName).AsEnumerable().FirstOrDefault();

                    return _PictureUser;
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
                    return Client.CreateDocumentQuery<Document>(Context.GetCollectionByName("PictureUsers").SelfLink)
                       .Where(D => D.Id == id)
                       .AsEnumerable()
                       .FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public PictureUser UpdatePictureUserByUsername(string userName, string email, List<string> pictureId)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    _PictureUser = GetPictureUserByUserName(userName);

                    if (_PictureUser == null)
                        return null;

                    Document document = GetDocumentById(_PictureUser.Id);
                    document.SetPropertyValue("Email", email);
                    document.SetPropertyValue("PictureId", pictureId);

                    Client.ReplaceDocumentAsync(document);

                    _PictureUser = GetPictureUserByUserName(userName);

                    return _PictureUser;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PictureUser DeletePictureUserByUserName(string userName)
        {

            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    _PictureUser = GetPictureUserByUserName(userName);
                    Document document = GetDocumentById(_PictureUser.Id);

                    if (_PictureUser == null)
                        return null;

                   var result = Client.DeleteDocumentAsync(document.SelfLink);

                    if (result.Result == null)
                        return null;

                    return _PictureUser;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
