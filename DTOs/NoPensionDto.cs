using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Aplicativo.net.DTOs
{
    public class ImportFIleDto
    {
        public IFormFile Archive { get; set; }
    }
}