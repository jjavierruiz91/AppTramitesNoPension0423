
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Aplicativo.net.Models;
using System.Threading.Tasks;
using System;

using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace Aplicativo.net.Services
{
  public class NopensionService
  {
    private readonly AplicativoContext _context;

    public NopensionService(AplicativoContext context)
    {
      _context = context;
    }

    public async Task loadUserUsingTask(List<ImportFile> dataExcel, string _qrPath)
    {
      try
      {

        foreach (var item in dataExcel)
        {
          var user = await _context.Nopension.FirstOrDefaultAsync(e => e.Identificacion == item.Identificacion.ToString());

          if (user != null) continue;
          string qrPath = _qrPath + item.Identificacion + Guid.NewGuid().ToString();
          var createUser = new nopension
          {
            Identificacion = item.Identificacion,
            Nombrecompleto = item.Nombrecompleto,
            estado = item.estado,
            estadoCertificado = "valido",
            fechaVencimiento = DateTime.Now,
            createdAt = DateTime.Now,
            updatedAt = DateTime.Now,
            totalDescargas = 0,
            token = "pension-" + Guid.NewGuid().ToString(),
            qrPath = qrPath
          };
          string url = "http://localhost:5001/public/validate?token=" + createUser.token;
          this.GenerarCodigoQR(url, qrPath);
          _context.Nopension.Add(createUser);
        }

        await _context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        Console.WriteLine(new { message = "Error al guardar la informacion del archivo excel" });
      }

      Console.WriteLine(new { message = "Usuario actualizado" });
    }

    public async Task updateUser(nopension user)
    {
      _context.Entry(user).State = EntityState.Modified;
      await _context.SaveChangesAsync();
    }

    public static bool ValidateDate(DateTime fechaIngresada, int diasValidos)
    {
      DateTime fechaActual = DateTime.Now;
      DateTime fechaLimite = fechaActual.AddDays(diasValidos);

      if (fechaIngresada <= fechaLimite)
      {
        return true;
      }

      return false;
    }

    public void GenerarCodigoQR(string texto, string rutaArchivo)
    {
      var width = 250; // width of the Qr Code
      var height = 250; // height of the Qr Code
      var margin = 0;
      var qrCodeWriter = new ZXing.BarcodeWriterPixelData
      {
        Format = ZXing.BarcodeFormat.QR_CODE,
        Options = new QrCodeEncodingOptions
        {
          Height = height,
          Width = width,
          Margin = margin
        }
      };
      var pixelData = qrCodeWriter.Write(texto);


      using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
      {
        using (var ms = new MemoryStream())
        {
          var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
          try
          {
            // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image
            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
          }
          finally
          {
            bitmap.UnlockBits(bitmapData);
          }

          // save to folder
          string fileGuid = Guid.NewGuid().ToString().Substring(0, 4);
          bitmap.Save(rutaArchivo + ".png", System.Drawing.Imaging.ImageFormat.Png);

          // save to stream as PNG
          bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        }

      }
    }

  }
}