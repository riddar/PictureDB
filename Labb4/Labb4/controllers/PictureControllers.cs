using Labb4.App_Start;
using Labb4.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Controllers
{
    public class PictureControllers
    {
        MongoContext Context = new MongoContext();
        Picture picture = new Picture();

        public Picture CreatePicture(string _id, string pictureName, string pictureUrl)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    var picture = Client.CreateDocumentQuery<Picture>(Context.GetCollectionByName("Pictures").SelfLink)
                        .Where(d => d.Id == _id).AsEnumerable().FirstOrDefault();
                    if (picture == null)
                    {
                        Picture newPicture = new Picture() { Id = _id, PictureName = pictureName, PictureUrl = pictureUrl, Valid = false };
                        Client.CreateDocumentAsync(Context.GetCollectionByName("Pictures").SelfLink, newPicture);
                        return newPicture;
                    }
                    else
                    {
                        return picture;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public IEnumerable<Picture> GetAllPictures()
        {     
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    IEnumerable<Picture> pictures = Client.CreateDocumentQuery<Picture>(Context.GetCollectionByName("Pictures").SelfLink).AsEnumerable();
                    List<Picture> list = new List<Picture>();
                    foreach (var picture in pictures)
                        list.Add(picture);
                    return list;
                }
               
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Picture GetPictureByPictureName(string pictureName)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    picture = Client.CreateDocumentQuery<Picture>(Context.GetCollectionByName("Pictures").SelfLink)
                                            .Where(d => d.PictureName == pictureName).AsEnumerable().FirstOrDefault();

                    return picture;
                }     
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public Picture GetPictureById(string id)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    picture = Client.CreateDocumentQuery<Picture>(Context.GetCollectionByName("Pictures").SelfLink)
                                               .Where(d => d.Id == id).AsEnumerable().FirstOrDefault();
                    return picture;
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
                    return Client.CreateDocumentQuery("Pictures")
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

        public Picture UpdatePictureDocument(string pictureName, string pictureUrl, bool valid)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    picture = Client.CreateDocumentQuery<Picture>(Context.GetCollectionByName("Pictures").SelfLink)
                        .Where(d => d.PictureName == pictureName).AsEnumerable().FirstOrDefault();

                    if (picture != null)
                    {
                        picture.PictureName = pictureName;
                        picture.PictureUrl = pictureUrl;
                        picture.Valid = valid;
                        Client.ReplaceDocumentAsync(Context.GetCollectionByName("Pictures").SelfLink, picture);
                        return picture;
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

        public void DeletePictureByPictureName(string pictureName)
        {
           
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    picture = Client.CreateDocumentQuery<Picture>(Context.GetCollectionByName("Pictures").SelfLink)
                    .Where(d => d.PictureName == pictureName).AsEnumerable().FirstOrDefault();

                    Document document = GetDocumentById(picture.Id);

                    if (picture != null)
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
