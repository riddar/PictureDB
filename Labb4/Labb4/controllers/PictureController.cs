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
    public class PictureController
    {
        CosmosDBContext Context = new CosmosDBContext();
        Picture picture = new Picture();

        public Picture CreatePicture(string pictureName, string pictureUrl)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    picture = Client.CreateDocumentQuery<Picture>(Context.GetCollectionByName("Pictures").SelfLink)
                        .Where(d => d.PictureName == pictureName).AsEnumerable().FirstOrDefault();

                    if (picture == null)
                    {
                        bool valid = false;
                        Picture newPicture = new Picture() {PictureName = pictureName, PictureUrl = pictureUrl, Valid = valid };
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
                    var doc =  Client.CreateDocumentQuery<Document>(Context.GetCollectionByName("Pictures").SelfLink)
                       .Where(D => D.Id == id)
                       .AsEnumerable()
                       .First();

                    return doc;
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

                        var document = GetDocumentById(picture.Id);
                        document.SetPropertyValue(document, picture);
                        var result = Client.ReplaceDocumentAsync(document.SelfLink, picture);
                        var test = result.IsCompleted;
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
