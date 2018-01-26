using Labb4.App_Start;
using Labb4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.Controllers
{
    public class AllowedPicturesControllers
    {
        MongoContext Context = new MongoContext("AllowedPictures");
        AllowedPictures AllowedPictures = new AllowedPictures();
    }
}
