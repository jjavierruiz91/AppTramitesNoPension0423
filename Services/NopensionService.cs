using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Aplicativo.net.DTOs;
using System.Threading.Tasks;
using System.Threading;
using System;


namespace Aplicativo.net.Services
{
  public class NopensionService
  {
     public async Task loadUserUsingTask(List<ImportFile> dataExcel, AplicativoContext _context)
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
            token = Guid.NewGuid().ToString(),
          };

          _context.Nopension.Add(createUser);
        }

        await _context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        BadRequest(new { message = "Error al guardar la informacion del archivo excel" });
      }

      Ok(new { message = "Usuario actualizado" });
    }
  }
}