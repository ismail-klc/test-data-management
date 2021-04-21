using Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Vibration : IMongoDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string TestID { get; set; }
        [BsonElement("ROS Time")]
        public string RosTime { get; set; }
        public char Axis { get; set; }
        public string Data { get; set; }
    }
}
