
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
  public class EnvironmentService
  {
    
    private readonly IWebHostEnvironment _environment;
    private readonly IConfiguracion _configuracion;

    public NopensionService(IConfiguracion configuracion,IWebHostEnvironment appEnvironment)
    {
      _environment = appEnvironment;
      _configuracion = configuraion
    }


    public string ObtenerPerfilActual(){
      string perfil = _environment.EnvironmentName;
      return perfil;
    }

  }
}