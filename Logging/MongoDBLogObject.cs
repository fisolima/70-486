using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Logging
{
    public class MongoDBLogObject
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public LogType LogType { get; set; }

        public string Context { get; set; }

        public string Scope { get; set; }

        public DateTime Created { get; set; }

        public IEnumerable<object> Objects { get; set; }
    }
}
