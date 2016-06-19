using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Logging
{
    public class MongoDBLogger : ILogger, IDisposable
    {
        public MongoClient Client { get; private set; }
        public IMongoDatabase Database { get; set; }
        public IMongoCollection<MongoDBLogObject> Collection { get; set; }

        public MongoDBLogger(string connectionString, string databaseName, string collectionName)
        {
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(databaseName);
            Collection = Database.GetCollection<MongoDBLogObject>(collectionName);
        }

        public void LogDebug(string context, string scope, params object[] objects)
        {
            Log(LogType.Debug, context, scope, objects);
        }

        public void LogInfo(string context, string scope, params object[] objects)
        {
            Log(LogType.Info, context, scope, objects);
        }

        public void LogWarning(string context, string scope, params object[] objects)
        {
            Log(LogType.Warning, context, scope, objects);
        }

        public void LogError(string context, string scope, params object[] objects)
        {
            Log(LogType.Error, context, scope, objects);
        }

        private void Log(LogType type, string context, string scope, params object[] objects)
        {
            try
            {
                Collection.InsertOne(new MongoDBLogObject()
                {
                    Context = context,
                    Scope = scope,
                    LogType = type,
                    Created = DateTime.UtcNow,
                    Objects = objects.ToList()
                });
            }
            catch (Exception exc) { }
        }

        public void Dispose()
        {
        }
    }
}
