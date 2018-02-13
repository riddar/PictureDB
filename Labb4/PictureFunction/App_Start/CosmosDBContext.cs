using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureFunction.App_Start
{
    public class CosmosDBContext
    {
        public readonly string EndpointUrl = "https://labb4.documents.azure.com:443";
        public readonly string Authkey = "NAfqsGRxrU29aYUWXuDkippbMKgeuNyiux6ZCTHVHxeZG3Em8aTHs8A3Ckn2K9E1MuudWjpBiJUFYYQxi2n2Cw==";
        public readonly string DatabaseName = "labb4";

        public Database GetDatabaseByName(string databaseName)
        {
            using (DocumentClient Client = new DocumentClient(new Uri(EndpointUrl), Authkey))
            {
                var Database = (from db in Client.CreateDatabaseQuery()
                                where db.Id == databaseName
                                select db).AsEnumerable().FirstOrDefault();

                return Database;
            }
        }

        public DocumentCollection GetCollectionByName(string CollectionName)
        {
            using (DocumentClient Client = new DocumentClient(new Uri(EndpointUrl), Authkey))
            {
                var Collection = (from col in Client.CreateDocumentCollectionQuery(GetDatabaseByName(DatabaseName).SelfLink)
                                  where col.Id == CollectionName
                                  select col).AsEnumerable().FirstOrDefault();
                return Collection;
            }
        }
    }
}
