using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDataAccess.Models
{
    public class ChoreModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ChoreText { get; set; }

        public int FrequencyInDays { get; set; }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public UserModel? AssignedTo { get; set; }

        public DateTime? LastComplete { get; set; }
    }
}
