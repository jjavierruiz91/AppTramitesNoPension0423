using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Aplicativo.net.DTOs
{
    public class ImportFIleDto
    {
        public IFormFile Archive { get; set; }
    }

    public class UpdateUserNoPension
    {
        public int Codsolicitante { get; set; }
        public string Identificacion { get; set; }
        public string Nombrecompleto { get; set; }
        public string estado { get; set; }
    }
}