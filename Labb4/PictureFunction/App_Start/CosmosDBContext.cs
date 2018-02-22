﻿using Microsoft.Azure.Documents;
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
        public readonly string EndpointUrl = "https://picturedb.documents.azure.com:443/";
        public readonly string Authkey = "cNKcjKztdIHcesct3cwYirohcuAuOHEEOGJRCLJHPq80X6hKYHXwJ3Ra101jj4dr48UlQD4yHuYHTjdkcFNLnA==";
        public readonly string DatabaseName = "Labb4";

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
