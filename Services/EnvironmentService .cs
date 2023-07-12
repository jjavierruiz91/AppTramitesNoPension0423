
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Aplicativo.net.Models;
using System.Threading.Tasks;
using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
 
namespace Aplicativo.net.Services
{
  public class EnvironmentService
  {
    
    private readonly IWebHostEnvironment _environment;
    private readonly IConfiguration _configuracion;

    public EnvironmentService(IConfiguration configuracion,IWebHostEnvironment appEnvironment)
    {
      _environment = appEnvironment;
      _configuracion = configuracion;
    }


    public string ObtenerPerfilActual(){
      string perfil = _environment.EnvironmentName;
      return perfil;
    } 

    public string ObtenerUrl(string profile)
    { 
        string url = _configuracion.GetValue<string>($"profiles:{profile}:applicationUrl");

        return url;
    } 

     public string ObtenerUrlDeProfile( )
    { 
        string profile = this.ObtenerPerfilActual(); 
        string url = _configuracion.GetValue<string>($"profiles:{profile}:applicationUrl"); 
        return url;
    } 
  }
}