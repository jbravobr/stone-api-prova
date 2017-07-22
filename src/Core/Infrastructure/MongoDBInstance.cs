using System;
using MongoDB.Driver;

namespace Domain.Core.Infrastructure
{
    public sealed class MongoDBInstance
    {
        static volatile MongoDBInstance instance;
        static object syncLock = new Object();
        //const string connectionString = "mongodb://localhost:27017/";
        const string connectionString = "mongodb://admin:admin@ds115583.mlab.com:15583/apistone";
        static IMongoDatabase db = null;

        MongoDBInstance()
        {
            var client = new MongoClient(connectionString);
            db = client.GetDatabase("apistone");
        }

        public static IMongoDatabase GetMongoDatabase
        {
            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        if (instance == null)
                            instance = new MongoDBInstance();
                    }
                }

                return db;
            }
        }
    }
}