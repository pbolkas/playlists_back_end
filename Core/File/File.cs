using System.IO;

namespace back_end.Core.File
{
  public class File
  {
    public static bool discardLargeFile(MemoryStream ms)
    {
      
      if (ms.ToArray().Length >  (int) SizeEnum.MB5)
      {
        return true;
      }

      return false;
    }

  }
}