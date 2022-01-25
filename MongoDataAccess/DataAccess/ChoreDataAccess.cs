using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDataAccess.Models;
using MongoDB.Driver;

namespace MongoDataAccess.DataAccess {

    public class ChoreDataAccess {

        private const string ConnectionString = "mongodb://127.0.0.1:27017";
        private const string DatabaseName = "choredb";
        private const string ChoreCollection = "chore_cart";
        private const string UserCollection = "users";
        private const string ChoreHistoryCollection = "chore_history";

       //Receives a collection of Type[ChoreCollection, UserCollection or ChoreHistoryCollection] that
       //was passed and returns it's collection from the DB.

       private IMongoCollection<T> ConnectToMongo<T>(in string collection) {

            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);

            return db.GetCollection<T>(collection); 
       }
        /* Get all users from the MongoDb */

        public async Task<List<UserModel>> GetAllUsers() {

            var usersCollection = ConnectToMongo<UserModel>(UserCollection);
            var results = await usersCollection.FindAsync(_ => true);

            return results.ToList();
       }

        /* Gets all chores from the MongoDb */

        public async Task<List<ChoreModel>> GetAllChores() {

            var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            var results = await choreCollection.FindAsync(_ => true);

            return results.ToList();
       }

        /* Gets All Chores for users from the MongoDb */

        public async Task<List<ChoreModel>> GetAllChoresForUser(UserModel user) {

            var choresCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            var results = await choresCollection.FindAsync(c => c.AssignedTo.Id == user.Id);

            return results.ToList();
       }

       /* Creates Users into the MongoDb */

       public Task CreateUser(UserModel user) {
            var usersCollection = ConnectToMongo<UserModel>(UserCollection);
            return usersCollection.InsertOneAsync(user);
       }

        /* Creates Chores into the MongoDb */

        public Task CreateChore(ChoreModel chore) {
            var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            return choreCollection.InsertOneAsync(chore);
        }

        /*
          Looks for the record to update and makes changes accordingly,
          if it doesn't find it, it makes an insert.
        */

        public Task UpdateChore(ChoreModel chore) {
            var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            var filter = Builders<ChoreModel>.Filter.Eq(field: "ID", chore.Id);
             
            return choreCollection.ReplaceOneAsync(filter, chore, options: new ReplaceOptions { IsUpsert = true}) ;
        }

        /* Deletes Chores from the MongoDb */

        public Task DeleteChore(ChoreModel chore) {
            var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            return choreCollection.DeleteOneAsync(c => c.Id == chore.Id);
        }

        /* Deletes Chores from the MongoDb */

        public async Task CompleteChore(ChoreModel chore) {
            //var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            //var filter = Builders<ChoreModel>.Filter.Eq(field: "Id", chore.Id);
            //await choreCollection.ReplaceOneAsync(filter, chore);

            //var choreHistoryCollection = ConnectToMongo<ChoreHistoryModel>(ChoreHistoryCollection);
            //await choreHistoryCollection.InsertOneAsync(new ChoreHistoryModel(chore));

            var client = new MongoClient(ConnectionString);
            using var session = await client.StartSessionAsync();

            session.StartTransaction();

            try {

                var db = client.GetDatabase(DatabaseName);
                var choreCollection = db.GetCollection<ChoreModel>(ChoreCollection);
                var filter = Builders<ChoreModel>.Filter.Eq(field: "Id", chore.Id);
            }
            catch (Exception ex) {

            }
        }
    }
}
