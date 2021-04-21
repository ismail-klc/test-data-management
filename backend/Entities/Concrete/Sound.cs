using Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Entities.Concrete
{
    public class Sound : IMongoDbEntity
    {

        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string TestID { get; set; }

        [BsonElement("ROS Time")]
        public string RosTime { get; set; }
        public string Data { get; set; }
    }
}
