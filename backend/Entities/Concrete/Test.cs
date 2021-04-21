using Core.DataAccess.MongoDb;
using Core.Entities;
using Core.Entities.Concrete;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    [BsonCollection("test")]
    public class Test : IMongoDbEntity
    {

        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public List<Sound1> Sound1 { get; set; }
        public List<Sound2> Sound2 { get; set; }
        public List<TestMain> Main { get; set; }
        public List<Vibration1> Vibration1 { get; set; }
        public List<Vibration2> Vibration2 { get; set; }
    }
}
