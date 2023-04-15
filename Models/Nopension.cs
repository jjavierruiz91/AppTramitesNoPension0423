using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Aplicativo.net.Models
{
//tabla de certificacion de no pension aca estan los datos de las personas que estan en la base de datos del departamento

 public class nopension
    {

      
  [Key] [JsonProperty("codsolicitante")] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Codsolicitante { get; set; }
      //   [Key] [JsonProperty("identificacion")] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Identificacion { get; set; }
        [JsonProperty("identificacion")] public string Identificacion { get; set; }
        [JsonProperty("nombrecompleto")] public string Nombrecompleto { get; set; }
        [JsonProperty("estado")] public string estado { get; set; }


 }

 public class ResponseNoPension
 {
  public FileStreamResult file { get; set; }
  public string estado { get; set; }
 }
}