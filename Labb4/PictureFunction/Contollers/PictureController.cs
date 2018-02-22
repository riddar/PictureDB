using PictureFunction.App_Start;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using PictureFunction.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PictureFunction.Controllers
{
    public class PictureController
    {
        CosmosDBContext Context = new CosmosDBContext();
        Picture _picture = new Picture();

        public Picture CreatePicture(string pictureName, string pictureUrl)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    _picture = GetPictureByPictureName(pictureName);

                    if (_picture == null)
                    {
                        Picture newPicture = new Picture() { PictureName = pictureName, PictureUrl = pictureUrl, Valid = false };
                        Client.CreateDocumentAsync(Context.GetCollectionByName("Pictures").SelfLink, newPicture);
                        return newPicture;
                    }
                    else
                    {
                        return _picture;
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

                    if (pictures == null)
                        return null;

                    foreach (var picture in pictures)
                        list.Add(picture);
                    return list;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Picture GetPictureByPictureName(string pictureName)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    _picture = Client.CreateDocumentQuery<Picture>(Context.GetCollectionByName("Pictures").SelfLink)
                                            .Where(d => d.PictureName == pictureName).AsEnumerable().FirstOrDefault();

                    if (_picture == null)
                        return null;

                    return _picture;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Picture GetPictureById(string id)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    _picture = Client.CreateDocumentQuery<Picture>(Context.GetCollectionByName("Pictures").SelfLink)
                                               .Where(d => d.Id == id).AsEnumerable().FirstOrDefault();

                    if (_picture == null)
                        return null;

                    return _picture;
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
                    Document doc = Client.CreateDocumentQuery<Document>(Context.GetCollectionByName("Pictures").SelfLink)
                       .Where(D => D.Id == id)
                       .AsEnumerable()
                       .First();

                    if (doc == null)
                        return null;

                    return doc;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public Picture UpdatePictureByPictureName(string pictureName, bool valid)
        {
            try
            {
                using (DocumentClient client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    _picture = GetPictureByPictureName(pictureName);

                    Document document = GetDocumentById(_picture.Id);
                    document.SetPropertyValue("Valid", valid);

                    client.ReplaceDocumentAsync(document);

                    _picture = GetPictureByPictureName(pictureName);

                    return _picture;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public Picture DeletePictureByPictureName(string pictureName)
        {
            try
            {
                using (DocumentClient Client = new DocumentClient(new Uri(Context.EndpointUrl), Context.Authkey))
                {
                    _picture = GetPictureByPictureName(pictureName);
                    Document document = GetDocumentById(_picture.Id);

                    if (_picture == null)
                        return null;

                    var result = Client.DeleteDocumentAsync(document.SelfLink);

                    if (result.Result == null)
                        return null;

                    return _picture;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
