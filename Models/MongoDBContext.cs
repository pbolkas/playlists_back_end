using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace back_end.Models
{
  
  public class MongoDBCRUD
  {
    private IMongoDatabase db;
    public MongoDBCRUD(string database)
    {
      var client = new MongoClient("");
      db = client.GetDatabase(database);
    }

    public async void InsertRecord<T>(string table, T record)
    {
      var collection = db.GetCollection<T>(table);
      await collection.InsertOneAsync(record);
    }

    public async Task<List<T>> LoadRecords<T>(string table)
    {
      var collection = db.GetCollection<T>(table);

      return await collection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<T> LoadRecordById<T>(string table,Guid id)
    {
      var collection = db.GetCollection<T>(table);
      
      var filter = Builders<T>.Filter.Eq("Id",id);

      return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public void UpsertRecord<T>(string table, Guid id, T record)
    {
      var collection = db.GetCollection<T>(table);

      var result = collection.ReplaceOne(
        new BsonDocument("_id",id),
        record,
        new ReplaceOptions { IsUpsert = true});
    }

    public void deleteRecord<T> (string table, Guid id)
    {    
      var collection = db.GetCollection<T>(table);

      var filter = Builders<T>.Filter.Eq("Id",id);

      collection.DeleteOne(filter);
    }
  }
}