using System;
using System.Linq;
using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;

namespace Tests
{
    internal class CustomObject
    {
        public string String { get; set; }
        public decimal Decimal { get; set; }
        public long Long { get; set; }
        public Guid Guid { get; set; }
        public DateTime DateTime { get; set; }

        public CustomObject()
        {
            Guid = Guid.NewGuid();
            Decimal = (decimal) Guid.GetHashCode();
            Long = Guid.GetHashCode();
            String = Guid.ToString();
            DateTime = DateTime.Now;
        }
    }

    [TestClass]
    public class MongoDBLogTest
    {
        [TestMethod]
        public void Can_Log_On_MongoDB()
        {
            var logger = new MongoDBLogger("mongodb://localhost:27017", "logs", "test_logs");

            var context = Guid.NewGuid().ToString();

            logger.LogDebug(context, "message");
            logger.LogInfo(context, "message");
            logger.LogWarning(context, "message");
            logger.LogError(context, "message");

            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("logs");
            var collection = database.GetCollection<MongoDBLogObject>("test_logs");

            var query = collection.FindAsync(l => l.Context == context).Result;

            var logs = query.ToList();

            Assert.IsTrue(logs.Count == 4);
        }

        [TestMethod]
        public void Can_Log_Complex_Objects()
        {
            var logger = new MongoDBLogger("mongodb://localhost:27017", "logs", "test_logs");

            var context = Guid.NewGuid().ToString();

            logger.LogInfo(context,
                "test",
                new CustomObject(), 
                new CustomObject(),
                "message to log"
            );

            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("logs");
            var collection = database.GetCollection<MongoDBLogObject>("test_logs");

            var query = collection.FindAsync(l => l.Context == context).Result;

            var logs = query.ToList();

            Assert.IsTrue(logs.Any());
        }
    }
}
