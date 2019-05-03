using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Dietician.CosmosDB
{
    public class CosmosRepository: ICosmosRepository
    { 
        private readonly DocumentClient _documentClient;
        private readonly string _databaseName;
        private readonly string _collectionName;

        public CosmosRepository(IAppConfiguration configuration)
        {
            _databaseName = configuration.GetVariable("cosmos.DatabaseName");
            _collectionName = configuration.GetVariable("cosmos.CollectionName");
            var endpointUri = configuration.GetVariable("cosmos.EndpointUri");
            var primaryKey = configuration.GetVariable("cosmos.PrimaryAuthorizationKey");

            _documentClient = new DocumentClient(new Uri(endpointUri), primaryKey);
        }

        public IOrderedQueryable GetDocuments(string ss)
        {
            if (ss == null)
            {
                throw new ArgumentNullException(nameof(ss));
            }

            var uri = UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName);
            var feedOptions = new FeedOptions {PartitionKey = new PartitionKey(ss)};

            return _documentClient.CreateDocumentQuery(uri, feedOptions);

        }

        public MealList GetAllMeals()
        {
            MealList list=new MealList();
            FeedOptions queryOptions = new FeedOptions {MaxItemCount = -1};


            IQueryable<MealList> familyQuery = this._documentClient.CreateDocumentQuery<MealList>(
                UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName));

            foreach (var meal in familyQuery)
            {
                list = meal;
            }

            return list;
        }
    }
}
