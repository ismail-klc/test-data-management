using Core.DataAccess.MongoDb;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    [BsonCollection("ses1")]
    public class Sound1 : Sound
    {
    }
}
