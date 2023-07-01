
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Aplicativo.net.Models;
using System.Threading.Tasks;
using System;
using QRCoder;
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

    public async Task loadUserUsingTask(List<ImportFile> dataExcel)
    {
      try
      {

        foreach (var item in dataExcel)
        {
          var user = await _context.Nopension.FirstOrDefaultAsync(e => e.Identificacion == item.Identificacion.ToString());

          if (user != null) continue;

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
          };

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


    public void GenerarCodigoQR(string url, string rutaArchivo)
    {
      QRCodeGenerator qrGenerator = new QRCodeGenerator();
      QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
      QRCode qrCode = new QRCode(qrCodeData);
      Bitmap qrCodeImage = qrCode.GetGraphic(20);

      qrCodeImage.Save(rutaArchivo, System.Drawing.Imaging.ImageFormat.Png);
    }


  }
}