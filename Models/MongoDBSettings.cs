namespace back_end.Models
{
  public interface IMongoDBSettings
  {
    string ConnectionString {get;set;}
    string CollectionName {get;set;}
  }
  public class MongoDBSettings: IMongoDBSettings
  {
    public string CollectionName {get;set;}
    public string ConnectionString {get;set;}
  }

}