using MongoDB.Bson;

namespace back_end.Entities
{
  public class SongMetaData : BsonDocument
  {
    public string Title {get;set;}
  }
}