
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

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

    public static List<ImportFile> ReadFile(IFormFile file)
    {
      List<ImportFile> list = new List<ImportFile>();

      Stream stream = file.OpenReadStream();

      IWorkbook MiExcel = null;

      MiExcel = new XSSFWorkbook(stream);

      ISheet HojaExcel = MiExcel.GetSheetAt(0);

      int cantidadFilas = HojaExcel.LastRowNum;


      for (int i = 1; i <= cantidadFilas; i++)
      {

        IRow fila = HojaExcel.GetRow(i);

        list.Add(new ImportFile
        {
          Identificacion = fila.GetCell(0).ToString(),
          Nombrecompleto = fila.GetCell(1).ToString(),
          estado = fila.GetCell(2).ToString(),
        });
      }

      return list;
    }


  }
}