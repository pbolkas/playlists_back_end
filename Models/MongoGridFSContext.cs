using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Collections.Generic;

namespace back_end.Models
{
  public class MongoGridFSContext
  {
    IMongoDatabase database;
    GridFSBucket bucket;

    public MongoGridFSContext(IOptions<MongoDBSettings> settings)
    {
      var client = new MongoClient(settings.Value.ConnectionString);
      bucket = new GridFSBucket(database);
    }

    public void storeFile<T>(string filename,T file)
    {
      
    }

    public byte[] retrieveFile(string filename, Guid id)
    {

    }
  }
}