using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class UserOperationClaim : IMongoDbEntity
    {
        public string UserId { get; set; }
        public string OperationClaimId { get; set; }
    }
}
