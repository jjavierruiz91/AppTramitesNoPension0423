
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using MiniExcelLibs;

public class ImportFile
{
  public string Identificacion { get; set; }
  public string Nombrecompleto { get; set; }
  public string estado { get; set; }
}


namespace Aplicativo.net.Utilities.FileHelper
{
  public class FileHelper
  {

    public static List<ImportFile> ReadFile(string basePath)
    {
      List<ImportFile> list = new List<ImportFile>();

      using (var reader = MiniExcel.GetReader(basePath, true))
      {
        while (reader.Read())
        {
          for (int i = 0; i < reader.FieldCount; i++)
          {
            list.Add((ImportFile)reader.GetValue(i));
          }
        }
      }

      return list;
    }


  }
}