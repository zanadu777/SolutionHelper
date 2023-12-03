using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemHelper
{
  public class KernalFile
  {

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DeleteFile(string lpFileName);

    public static void DeleteAllFiles(string path)
    {
      string[] files = (string[]) Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories);
      foreach (string file in files)
      {
        DeleteFile(file);
      }
      Directory.Delete(path, true);
    }

  }
}
