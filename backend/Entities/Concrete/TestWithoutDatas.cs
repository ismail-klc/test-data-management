using Core.DataAccess.MongoDb;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    [BsonCollection("test")]
    public class TestWithoutDatas : IMongoDbEntity
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
    }
}
