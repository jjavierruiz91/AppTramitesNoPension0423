
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
      Console.WriteLine(profile);
       Console.WriteLine($"{profile}:applicationUrl");
      
       // string url = _configuracion.GetValue<string>($"{profile}:applicationUrl");
        string url = _configuracion[$"{profile}"];
        string url1 = _configuracion.GetValue<string>(profile);
           
        Console.WriteLine(url);
        Console.WriteLine(url1);
         if (string.IsNullOrEmpty(url1))
        {
            throw new InvalidOperationException($"La URL para el perfil '{profile}' no está configurada.");
        }

         Console.WriteLine(url);
        return url;
    } 

     public string ObtenerUrlDeProfile( )
    { 
        string profile = this.ObtenerPerfilActual(); 
        Console.WriteLine(profile);
        string url = this.ObtenerUrl(profile);
        return url;
    } 
  }
}