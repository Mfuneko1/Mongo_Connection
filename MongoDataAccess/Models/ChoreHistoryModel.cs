using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDataAccess.Models
{
    public class ChoreHistoryModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ChoreId { get; set; }

        public string ChoreText { get; set; }

        public int FrequencyInDays { get; set; }

        public UserModel WhoCompleted { get; set; }

        public DateTime DateCompleted { get; set; }

        public ChoreHistoryModel()
        {

        }

        public ChoreHistoryModel(ChoreModel choreModel)
        {
            ChoreId = choreModel.Id;
            DateCompleted = choreModel.LastComplete ?? DateTime.Now;
            WhoCompleted = choreModel.AssignedTo;
            ChoreText = choreModel.ChoreText;
        }
    }
}
