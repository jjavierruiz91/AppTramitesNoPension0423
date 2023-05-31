using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Aplicativo.net.Models;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Microsoft.AspNetCore.Hosting;
using Aplicativo.net.Utilities.FileHelper;
using Aplicativo.net.DTOs;
 
using System.Threading;
using Microsoft.Extensions.Hosting;


namespace Aplicativo.net.Controllers
{
  [Route("api/[Controller]")]
  [ApiController]
  public class NopensionController : ControllerBase
  {
    private readonly AplicativoContext _context;
    private readonly IWebHostEnvironment _appEnvironment;
    private readonly IConfiguration _config;
   
    private readonly int records = 10;
    public NopensionController(AplicativoContext context, IWebHostEnvironment appEnvironment, IConfiguration config)
    {
      _context = context;
      _appEnvironment = appEnvironment;
      _config = config;
    }


    [HttpGet]
    public async Task<IActionResult> GetTramites([FromQuery] int? page)
    {
      int _page = page ?? 1;
      int totalRecords = await _context.Nopension.CountAsync();
      int total_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRecords / records)));
      var pensiones = await _context.Nopension.Skip((_page * 1) * records).Take(records).ToListAsync();

      return Ok(
        new
        {
          pages = total_pages,
          records = pensiones,
          total_records = totalRecords,
          current_page = _page
        });
    }


    [HttpPost]
    public async Task<IActionResult> ImportFile([FromForm] ImportFIleDto request)
    {
      var path = _appEnvironment.ContentRootPath;
      var ruta = _config.GetSection("routeImportFile").Value + DateTime.Now.Month + Guid.NewGuid().ToString("N") + ".xlsx";
      var staticPath = Path.Combine(path, ruta);

      using (var stream = new FileStream(staticPath, FileMode.Create))
      {
        request.Archive.CopyTo(stream);
      }
      FileHelper a = new FileHelper();

      var dataExcel = a.ReadFile(request.Archive);

         try
    {
      
       foreach (var item in dataExcel)
        {
       var user = await _context.Nopension.FirstOrDefaultAsync(e => e.Identificacion == item.Identificacion.ToString());
         
         if(user != null) continue;
       
        var createUser = new nopension{
            Identificacion = item.Identificacion,
            Nombrecompleto = item.Nombrecompleto,
            estado = item.estado
        };
      
        _context.Nopension.Add(createUser);
      }  

       await _context.SaveChangesAsync();
      
      a.deleteFile(staticPath);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al consultar el objeto por ID: {ex.Message}");
      
    } 
      return  Ok(new { mensaje = "Se guardo correctamente los usuario" });
    }

    private DbContextOptions<AplicativoContext> GetDbContextOptions()
{
    // Aquí debes proporcionar las opciones adecuadas para la configuración de tu contexto
    // Puedes utilizar métodos de configuración como UseSqlServer, UseInMemoryDatabase, etc.
    // Ejemplo:
    var optionsBuilder = new DbContextOptionsBuilder<AplicativoContext>();
    optionsBuilder.UseSqlServer(_config.GetConnectionString("SQLConnection")); // Reemplaza "cadena_de_conexion" con tu cadena de conexión real
    return optionsBuilder.Options;
}


    // GET: api/Task/5
    [HttpGet("certificado-nopension/{id}")]
    public async Task<ActionResult> GetNopension(string id)
    {
      var clienteItem = await _context.Nopension.FirstOrDefaultAsync(e => e.Identificacion == id);

      if (clienteItem == null)
      {
        return StatusCode(401, new { message = "El usuario que intenta buscar no fue encontrado", code = 401 });
      }

      if (clienteItem.estado == "jubilado")
      {
        return StatusCode(402, new { message = "El usuario registrado se encuentra Jubilado", code = 402 });
      }


      var path = _appEnvironment.ContentRootPath;
      //var ruta = "ClientApp//src//assets//Documentos//"+ clienteItem.Identificacion + ".pdf";
      var ruta = _config.GetSection("routeFileBase").Value + clienteItem.Identificacion + ".pdf";

      var filePath = Path.Combine(path, ruta);

      if (System.IO.File.Exists(filePath))
      {
        System.IO.File.Delete(filePath);
      }

      System.IO.FileStream fs = new FileStream(filePath, FileMode.Create);

      // Create an instance of the document class which represents the PDF document itself.  
      Document document = new Document(PageSize.LETTER);
      PdfWriter writer = PdfWriter.GetInstance(document, fs);
      document.AddAuthor("Gobernacion");

      document.Open();
      //var header = "ClientApp//src//assets//Imagenes//nopension//logos.png";

      var header_logo_left = _config.GetSection("routeFileImages").Value + "nopension//logo_depto.png";
      var header_logo_right = _config.GetSection("routeFileImages").Value + "nopension//logo_cesar.png";

      //var footer = "ClientApp//src//assets//Imagenes//nopension//footer.png";
      iTextSharp.text.Image img_header_left = iTextSharp.text.Image.GetInstance(header_logo_right);
      iTextSharp.text.Image img_header_right = iTextSharp.text.Image.GetInstance(header_logo_left);
      //img_header.ScaleAbsolute(50, 145);
      //img_header.SetAbsolutePosition(7, 50);
      img_header_left.ScaleAbsoluteWidth(95);
      img_header_left.ScaleAbsoluteHeight(95);
      // img_header_left.SetAbsolutePosition(0, 675);
      document.Add(img_header_left);

      img_header_right.ScaleAbsoluteWidth(95);
      img_header_right.ScaleAbsoluteHeight(95);
      img_header_right.SetAbsolutePosition(490, 665);
      document.Add(img_header_right);

      // Add a simple and wellknown phrase to the document in a flow layout manner  
      document.Add(new Paragraph(" "));
      Paragraph p1 = new Paragraph("Valledupar " + DateTime.Now.ToString());
      p1.Alignment = Element.ALIGN_LEFT;
      p1.SetLeading(1, 1);
      document.Add(p1);

      Paragraph p2 = new Paragraph("\n\n EL LIDER DE LA OFICINA DE GESTION HUMANA DE LA  GOBERNACION \n DEL  DEPARTAMENTO DEL CESAR");
      p2.Alignment = Element.ALIGN_CENTER;
      p2.SetLeading(2, 2);
      document.Add(p2);

      document.Add(new Paragraph("\n \n"));
      Paragraph p3 = new Paragraph("CERTIFICA");
      p3.Alignment = Element.ALIGN_CENTER;
      p3.SetLeading(1, 1);
      document.Add(p3);

      Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
      Font font_subTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11f);
      Font font_paragraph = FontFactory.GetFont("Arial", 10f, BaseColor.BLACK);
      Font font_paragraph_size_10 = FontFactory.GetFont("Arial", 10f, BaseColor.BLACK);

      Paragraph p5 = new Paragraph();
      p5.Alignment = Element.ALIGN_JUSTIFIED;
      p5.SetLeading(1, 2);
      p5.Add("Que, según revisión de nómina de pensionados de esta entidad,se constató que el Ciudadano(a)  ");
      p5.Add(new Chunk(clienteItem.Nombrecompleto, boldFont));
      p5.Add(" identificado con la Cédula de ciudadanía  No. ");
      p5.Add(new Chunk(clienteItem.Identificacion, boldFont));
      p5.Add("\t\t NO RECIBE PENSION ALGUNA.");
      document.Add(p5);

      document.Add(new Paragraph(" "));
      document.Add(new Paragraph(" "));
      document.Add(new Paragraph(" "));

      Chunk lineBreakFirma = new Chunk(new LineSeparator(1f, 45f, BaseColor.BLACK, Element.ALIGN_CENTER, -1));

      var firma_lina_maria = _config.GetSection("routeFileImages").Value + "firma_lina_fernanda.png";
      iTextSharp.text.Image img_firma_lina_maria = iTextSharp.text.Image.GetInstance(firma_lina_maria);

      //Teniendo en cuenta los cambios

      document.Add(lineBreakFirma);
      Paragraph p11 = new Paragraph("");
      p11.Add(new Chunk("LINA MARÍA FERNANDEZ CUELLO\n", font_subTitle));
      p11.Add(new Phrase("Líder Programa de Gestión Humana", font_paragraph_size_10));
      p11.Alignment = Element.ALIGN_CENTER;
      document.Add(p11);

      document.Add(new Paragraph(" "));
      document.Add(new Paragraph(" "));

      //PdfPTable table = new PdfPTable(new float[] { 55f, 400f  }) {  WidthPercentage = 100 };
      //PdfPCell cell1 = new PdfPCell(new Phrase("Proyecto:", font_paragraph_size_10));
      //PdfPCell cell2 = new PdfPCell(new Phrase("Karelys Rios Maestre - Profesional-Contratista", font_paragraph_size_10));
      //table.AddCell(cell1);
      //table.AddCell(cell2);

      //PdfPCell cell = new PdfPCell(new Phrase("El arriba firmante declara que el documento proyectado se encuentra ajustado a las disposiciones legales, por lo cual bajo mi responsabilidad presento para firma.",font_paragraph_size_10));
      //cell.Rowspan = 1;
      //cell.Colspan = 2;
      //table.AddCell(cell);
      //document.Add(table);

      // Paragraph p6 = new Paragraph("\nProyecto: Karelys Rios Maestre - Profesional-Contratista");
      //  p6.Alignment = Element.ALIGN_JUSTIFIED;
      //  p6.SetLeading(2, 2);
      //   document.Add(p6);   

      // Paragraph p7 = new Paragraph("El arriba firmante declara que el documento proyectado se encuentra ajustado a las disposiciones legales, por lo cual bajo mi responsabilidad presento para firma.");
      //  p7.Alignment = Element.ALIGN_JUSTIFIED;
      // p7.SetLeading(1, 1);
      //   document.Add(p7);
      document.Add(new Paragraph(" "));

      Paragraph p10 = new Paragraph(new Chunk("NOTA: Esta certificación se ha expedido a través de nuestro Módulo de Autoservicio a Empleados, por lo que NO es válida a menos que se confirme en el siguiente teléfono en Valledupar (Ces) 5748230 EXT: 313 -319 - 314 - 315.", font_paragraph));
      p10.Alignment = Element.ALIGN_JUSTIFIED;
      p10.SetLeading(1, 1);
      document.Add(p10);

      document.Add(new Paragraph(" "));

      Chunk lineBreak = new Chunk(new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -1));
      document.Add(lineBreak);

      Paragraph p9 = new Paragraph("");
      p9.Alignment = Element.ALIGN_CENTER;
      p9.SetLeading(1, 1);
      p9.Add(new Chunk("Horario de Atención:", font_subTitle));
      p9.Add(new Phrase("Lunes a Viernes de 07:45 am - 12:45 pm y 02:45 pm - 05:45 pm \n ", font_paragraph_size_10));

      p9.Add(new Chunk("Dirección: ", font_subTitle));
      p9.Add(new Phrase("Calle 16 # 12 - 120 Edificio Alfonso López Michelsen Valledupar - Cesar - Colombia \n", font_paragraph_size_10));

      p9.Add(new Chunk("Líneas de Atención a la Ciudadanía: ", font_subTitle));
      p9.Add(new Phrase("01 8000 954 099 / (575) 5748230 \n", font_paragraph_size_10));

      p9.Add(new Chunk("Correo Institucional: ", font_subTitle));
      p9.Add(new Phrase("contactenos@cesar.gov.co \n", font_paragraph_size_10));

      p9.Add(new Chunk("Código Postal:", font_subTitle));
      p9.Add(new Phrase("200001 \n", font_paragraph_size_10));

      p9.Add(new Chunk("Nit: ", font_subTitle));
      p9.Add(new Phrase("892399999-1\n", font_paragraph_size_10));

      p9.Add(new Chunk("Políticas de Privacidad y Condiciones de Uso de la Información: ", font_subTitle));
      document.Add(p9);

      iTextSharp.text.Image img_logo_center = iTextSharp.text.Image.GetInstance(_config.GetSection("routeFileImages").Value + "nopension//logo_center.png");

      img_logo_center.ScaleAbsoluteWidth(500);
      img_logo_center.ScaleAbsoluteHeight(500);
      img_logo_center.SetAbsolutePosition(50, 190);
      //img_logo_center.ScaleToFit(1700, 1000); 
      document.Add(img_logo_center);

      img_firma_lina_maria.ScaleAbsoluteWidth(140);
      img_firma_lina_maria.ScaleAbsoluteHeight(140);
      img_firma_lina_maria.SetAbsolutePosition(225, 300);
      document.Add(img_firma_lina_maria);

      //iTextSharp.text.Image img_footer = iTextSharp.text.Image.GetInstance(footer);  
      //img_footer.ScaleAbsolute(82, 40);
      //img_footer.SetAbsolutePosition(3, 200); 

      //img_footer.ScaleAbsoluteWidth(450);
      //img_footer.ScaleAbsoluteHeight(180);
      //img_footer.SetAbsolutePosition(90f,20f);
      // Close the document  
      document.Close();
      // Close the writer instance  
      writer.Close();
      // Always close open filehandles explicity  
      fs.Close();

      //System.IO.FileStream fileStream =  System.IO.File.OpenRead(filePath); 
      byte[] fileContent = System.IO.File.ReadAllBytes(filePath);
      return new FileContentResult(fileContent, "application/pdf");
      //var fileUpload = File(fileStream, "application/octet-stream", "{{filename.ext}}");
      //System.IO.File.Delete(filePath);
      //fileStream.Close();
      //return fileUpload;
    }

  }
}