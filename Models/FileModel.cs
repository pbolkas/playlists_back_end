using System;

namespace back_end.Models
{
  public class FileModel
  {
    public Guid fileId {get;set;}
    public byte [] fileBytes {get;set;}
  }
}