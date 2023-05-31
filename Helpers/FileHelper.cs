
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

  public ImportFile(string identificacion, string nombrecompleto, string estado)
  {
    this.Identificacion = identificacion;
    this.Nombrecompleto = nombrecompleto;
    this.estado = estado;
  }

}


namespace Aplicativo.net.Utilities.FileHelper
{
  public class FileHelper
  {
      
    public List<ImportFile> ReadFile(IFormFile file)
    {
      List<ImportFile> list = new List<ImportFile>();

      if (file != null)
      {
        Stream stream = file.OpenReadStream();

        if (stream != null)
        {
          IWorkbook MiExcel = new XSSFWorkbook(stream);
          ISheet HojaExcel = MiExcel.GetSheetAt(0);
          int cantidadFilas = HojaExcel.LastRowNum;
        
          for (int i = 1; i <= cantidadFilas; i++)
          {

            IRow fila = HojaExcel.GetRow(i);
             
            string identificacion = fila.GetCell(0)?.ToString() ?? string.Empty;
            string nombrecompleto = fila.GetCell(1)?.ToString() ?? string.Empty;
            string estado = fila.GetCell(2)?.ToString() ?? string.Empty;

            ImportFile import2 = new ImportFile(identificacion, nombrecompleto, estado);
            list.Add(import2);
          }

        }
        else
        {
          Console.WriteLine("Manejo de error: el flujo de lectura no se pudo abrir");
        }
      }
      else
      {
        Console.WriteLine("Manejo de error: el archivo no se proporcionó correctamente");

      }

      return list;
    }


  }
}