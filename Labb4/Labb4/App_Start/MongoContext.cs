using Labb4.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4.App_Start
{
    public class MongoContext
    {
        private static readonly string EndpointUrl = ConfigurationManager.AppSettings["EndpointUrl"];
        private static readonly string Authkey = ConfigurationManager.AppSettings["AuthKey"];
        private static readonly string DatabaseName = ConfigurationManager.AppSettings["DatabaseName"];

        public DocumentClient Client { get; set; }
        public IEnumerable<Database> Databases { get; set; }
        public Database Database { get; set; }
        public IEnumerable<DocumentCollection> Collections { get; set; }
        public DocumentCollection Collection { get; set; }

        public MongoContext(string CollectionName)
        {
            try
            {
                using (Client = new DocumentClient(new Uri(EndpointUrl), Authkey))
                {
                    Databases = from db in Client.CreateDatabaseQuery() select db;

                    Database = (from db in Client.CreateDatabaseQuery()
                                where db.Id == DatabaseName
                                select db).AsEnumerable().FirstOrDefault();

                    Collections = from col in Client.CreateDocumentCollectionQuery(Database.SelfLink) select col;

                    Collection = (from col in Client.CreateDocumentCollectionQuery(Database.SelfLink)
                                  where col.Id == CollectionName
                                  select col).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
