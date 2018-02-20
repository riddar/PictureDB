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
        PictureUser pictureUser = new PictureUser();

        public PictureUser CreatePictureUser(string userName, string email, List<string> pictureId)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    pictureUser = Client.CreateDocumentQuery<PictureUser>(Context.GetCollectionByName("PictureUsers").SelfLink)
                        .Where(d => d.Username == userName).AsEnumerable().FirstOrDefault();

                    if (pictureUser == null)
                    {
                        PictureUser newPictureUser = new PictureUser() { Username = userName, Email = email, PictureId = pictureId };
                        Client.CreateDocumentAsync(Context.GetCollectionByName("PictureUsers").SelfLink, newPictureUser);
                        return newPictureUser;
                    }
                    else
                    {
                        return pictureUser;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public IEnumerable<PictureUser> GetAllPictureUser()
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    List<PictureUser> list = new List<PictureUser>();
                    IEnumerable<PictureUser> pictureUsers = Client.CreateDocumentQuery<PictureUser>(Context.GetCollectionByName("PictureUsers").SelfLink).AsEnumerable();
                    foreach (var pictureUser in pictureUsers )
                    {
                        list.Add(pictureUser);
                    }
                    return list;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public PictureUser GetPictureUserByUserName(string userName)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    pictureUser = Client.CreateDocumentQuery<PictureUser>(Context.GetCollectionByName("PictureUsers").SelfLink)
                                            .Where(d => d.Username == userName).AsEnumerable().FirstOrDefault();

                    return pictureUser;
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
                    return Client.CreateDocumentQuery<Document>(Context.GetCollectionByName("PictureUsers").SelfLink)
                       .Where(D => D.Id == id)
                       .AsEnumerable()
                       .FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }

        }

        public PictureUser UpdatePictureUserDocument(string userName, string email, List<string> pictureId)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    pictureUser = Client.CreateDocumentQuery<PictureUser>(Context.GetCollectionByName("PictureUsers").SelfLink)
                        .Where(d => d.Username == userName).AsEnumerable().FirstOrDefault();

                    if (pictureUser == null)
                    {
                        PictureUser newPictureUser = new PictureUser() { Username = userName, Email = email, PictureId = pictureId };
                        Client.CreateDocumentAsync(Context.GetCollectionByName("PictureUsers").SelfLink, newPictureUser);
                        return newPictureUser;
                    }
                    else
                    {
                        pictureUser.Username = userName;
                        pictureUser.Email = email;
                        pictureUser.PictureId = pictureId;

                        Document document = GetDocumentById(pictureUser.Id);

                        Client.ReplaceDocumentAsync(document.SelfLink, pictureUser);
                        return pictureUser;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public PictureUser DeletePictureUserByUserName(string userName)
        {

            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    pictureUser = Client.CreateDocumentQuery<PictureUser>(Context.GetCollectionByName("PictureUsers").SelfLink)
                    .Where(d => d.Username == userName).AsEnumerable().FirstOrDefault();

                    Document document = GetDocumentById(pictureUser.Id);

                    if (pictureUser != null)
                    {
                        Client.DeleteDocumentAsync(document.SelfLink);
                        return pictureUser;
                    }
                    else
                    {
                        return null;
                    }

                    
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
