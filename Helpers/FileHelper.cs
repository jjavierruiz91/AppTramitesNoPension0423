
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using MiniExcelLibs;

public class ImportFile
{
  public string nombre { get; set; }
  public string identificado { get; set; }
 // public string estado { get; set; }
}


namespace Aplicativo.net.Utilities.FileHelper
{
  public class FileHelper
  {

    public static List<ImportFile> ReadFile(string basePath)
    {
      List<ImportFile> list = new List<ImportFile>();
      using (var stream = File.OpenRead(basePath))
      {
          var rows = stream.Query().ToList();
                      
      
        
      Console.WriteLine(rows[0].nombre);
          
      }
         
      using (var reader = MiniExcel.GetReader(basePath, true))
      {
        while (reader.Read())
        {
        
          for (int i = 0; i < reader.FieldCount; i++)
          {
            
              Console.WriteLine(i);
          
             
          }
        }
      }
      return list;
    }


  }
}