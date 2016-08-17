using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace NLog.MongoDB20
{
    using System.Collections.Generic;

	internal sealed class MongoRepository : IRepository
	{
		private MongoClient _client;
		private readonly string _database;

		private static readonly Dictionary<string, bool> _collectionCache = new Dictionary<string, bool>();

        public MongoRepository(MongoClientSettings settings, string databaseName)
	    {
            _database = databaseName;
            _client = new MongoClient(settings);
;
        }

        public void CheckCollection(string collectionName, long collectionSize, long? collectionMaxItems, bool createIdField)
        {
            var db = _client.GetDatabase(_database);

            lock (_collectionCache)
            {
                if (_collectionCache.ContainsKey(collectionName)) return;

                //Not really happy with this implementation in order to determine if a collection exists.
                bool collectionExists = false;

                using (var cursor = db.ListCollections())
                {
                    var collections = cursor.ToList();
                    foreach (var collection in collections)
                    {
                        if (collection["name"] == collectionName)
                        {
                            collectionExists = true;
                        }       
                    }
                }


                if (!collectionExists)
                {
                    var collectionOptions = new CreateCollectionOptions()
                    {
                        Capped = true,
                        MaxSize = collectionSize,
                        AutoIndexId = createIdField
                    };

                    if (collectionMaxItems.HasValue)
						collectionOptions.MaxDocuments =collectionMaxItems.Value;

                    db.CreateCollection(collectionName, collectionOptions);


                }

                _collectionCache.Add(collectionName, createIdField);
            }

        }

	    public void Insert(string collectionName, BsonDocument item)
		{
			var db = _client.GetDatabase(_database);

		    IMongoCollection<BsonDocument> collection;

			// if we shouldn't save ids
		    if (_collectionCache.ContainsKey(collectionName) && !_collectionCache[collectionName])
			    collection = db.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings {AssignIdOnInsert = false});
			else
				collection = db.GetCollection<BsonDocument>(collectionName);

            collection.InsertOne(item);
        }

		public void Dispose()
		{
			_client = null;
		}
	}
}