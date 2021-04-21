using Core.DataAccess.MongoDb;
using Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    [BsonCollection("main")]
    public class TestMain : IMongoDbEntity
    {
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string TestID { get; set; }
        [BsonElement("ROS Time")]
        public string RosTime { get; set; }
        [BsonElement("Robot Bas Acisi")]
        public string RobotBasAcisi { get; set; }
        [BsonElement("Ortam Sicakligi")]
        public string OrtamSicakligi { get; set; }
        [BsonElement("Konum x")]
        public string KonumX { get; set; }
        [BsonElement("Konum y")]
        public string KonumY { get; set; }
        [BsonElement("D_Konum x")]
        public string D_KonumX { get; set; }
        [BsonElement("D_Konum y")]
        public string D_KonumY { get; set; }
        [BsonElement("Plab Konum x")]
        public string PlabKonumX { get; set; }
        [BsonElement("Plab Konum y")]
        public string PlabKonumY { get; set; }
        [BsonElement("Motor1 Cekim Akimi")]
        public string Motor1CekimAkimi { get; set; }
        [BsonElement("Motor1 Teker Hizi")]
        public string Motor1TekerHizi { get; set; }
        [BsonElement("Motor1 Guc")]
        public string Motor1Guc { get; set; }
        [BsonElement("Motor1 Aku Gerilim")]
        public string Motor1AkuGerilim { get; set; }
        [BsonElement("Motor1 Sicaklik")]
        public string Motor1Sicaklik { get; set; }
        [BsonElement("Motor1 Nem")]
        public string Motor1Nem { get; set; }

        [BsonElement("Motor2 Cekim Akimi")]
        public string Motor2CekimAkimi { get; set; }
        [BsonElement("Motor2 Teker Hizi")]
        public string Motor2TekerHizi { get; set; }
        [BsonElement("Motor2 Guc")]
        public string Motor2Guc { get; set; }
        [BsonElement("Motor2 Aku Gerilim")]
        public string Motor2AkuGerilim { get; set; }
        [BsonElement("Motor2 Sicaklik")]
        public string Motor2Sicaklik { get; set; }
        [BsonElement("Motor2 Nem")]
        public string Motor2Nem { get; set; }
    }
}
