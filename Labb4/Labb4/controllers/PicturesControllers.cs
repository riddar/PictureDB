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
    public class PicturesControllers
    {
        MongoContext Context = new MongoContext("Pictures");

        public Picture CreatePicture(string _id, string pictureName, string pictureUrl)
        {
            try
            {
                Picture picture = Context.Client.CreateDocumentQuery<Picture>(Context.Collection.SelfLink)
                    .Where(d => d._Id == _id).AsEnumerable().FirstOrDefault();

                if (picture == null)
                {
                    Picture newPicture = new Picture() { _Id = _id, PictureName = pictureName, PictureUrl = pictureUrl, Valid = false };
                    Context.Client.CreateDocumentAsync(Context.Collection.SelfLink, newPicture);
                    return newPicture;

                }
                else
                {
                    return picture;
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
                IEnumerable<Picture> pictures = Context.Client.CreateDocumentQuery<Picture>(Context.Collection.DocumentsLink).AsEnumerable();
                return pictures;
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
                Picture picture = Context.Client.CreateDocumentQuery<Picture>(Context.Collection.DocumentsLink)
                    .Where(d => d.PictureName == pictureName).AsEnumerable().FirstOrDefault();

                return picture;
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

        public Picture UpdatePictureDocument(string pictureName, string pictureUrl, bool valid)
        {
            try
            {
                var picture = Context.Client.CreateDocumentQuery<Picture>(Context.Collection.SelfLink)
                    .Where(d => d.PictureName == pictureName).AsEnumerable().FirstOrDefault();

                if (picture != null)
                {
                    picture.PictureName = pictureName;
                    picture.PictureUrl = pictureUrl;
                    picture.Valid = valid;
                    Context.Client.ReplaceDocumentAsync(Context.Collection.SelfLink, picture);
                    return picture;
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

        public void DeletePictureByPictureName(string pictureName)
        {
            try
            {
                var Picture = Context.Client.CreateDocumentQuery<Picture>(Context.Collection.SelfLink)
                    .Where(d => d.PictureName == pictureName).AsEnumerable().FirstOrDefault();

                Document document = GetDocumentById(Picture._Id);

                if (Picture != null)
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
