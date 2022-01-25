using System;
using MongoDataAccess.DataAccess;
using MongoDB.Driver;
using MongoDbDemo;
using MongoDataAccess.Models;
using System.Linq;

//string connectionString = "mongodb://127.0.0.1:27017";
//string databaseName = "simple_db";
//string collectionName = "people";

//var client = new MongoClient(connectionString);
//var db = client.GetDatabase(databaseName);
//var collection = db.GetCollection<PersonModel>(collectionName);

//var person = new PersonModel { FirstName = "Mfuneko", LastName = "April" };

////Inserting the values into the DB 
//await collection.InsertOneAsync(person);

////Line to return every record from the DB where there's true
//var results = await collection.FindAsync(_ => true);

//foreach (var item in results.ToList()) {
//  Console.WriteLine($"{item.Id}: {item.FirstName} {item.LastName}");
//}


ChoreDataAccess db = new ChoreDataAccess();

await db.CreateUser(user: new UserModel() { FirstName = "Mfuneko", LastName = "April" });

var users = await db.GetAllUsers();



var chores = new ChoreModel() { AssignedTo = users.First(), ChoreText = "Wash Dishes", FrequencyInDays = 3};

await db.CreateChore(chores);
