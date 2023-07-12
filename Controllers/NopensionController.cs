using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Threading.Tasks;
using System;
using Aplicativo.net.Models;

using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Microsoft.AspNetCore.Hosting;
using Aplicativo.net.Utilities.FileHelper;
using Aplicativo.net.DTOs;


using Aplicativo.net.Services;


namespace Aplicativo.net.Controllers
{
  [Route("api/[Controller]")]
  [ApiController]
  public class NopensionController : ControllerBase
  {
    private readonly AplicativoContext _context;
    private readonly IWebHostEnvironment _appEnvironment;
    private readonly IConfiguration _config;

  private readonly NopensionService _nopensionService;


    private readonly int records = 10;
    string[] columnNames = { "identificacion", "nombrecompleto", "estado" };
    string[] allowTypes = { ".csv", ".xlsx" };

    public NopensionController(AplicativoContext context, IWebHostEnvironment appEnvironment, IConfiguration config, NopensionService nopensionService)
    {
      _context = context;
      _appEnvironment = appEnvironment;
      _config = config;
      _nopensionService = nopensionService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<nopension>> GetUserNoPension(string id)
    {
      var user = await _context.Nopension.FirstOrDefaultAsync(e => e.Identificacion == id);

      if (user == null)
      {
        return BadRequest(new { message = "Usuario no encontrado" });
      }

      return user;
    }

    [HttpGet("validate/{token}/token")]
    public async Task<ActionResult> GetValidateQr(string token)
    {
      var user = await _context.Nopension.FirstOrDefaultAsync(e => e.token == token);

      if (user == null)
      {
        return BadRequest(new { message = "Token no fue encontrado!" });
      }

      bool dateValida = NopensionService.ValidateDate(user.fechaVencimiento, 30);
      
      var newObjet = new { status = dateValida };
      if(dateValida) {
        return Ok(new {
            status = dateValida,
            token = dateValida ? user.token : "", 
            Nombrecompleto = user.Nombrecompleto, 
            Identificacion = user.Identificacion,
            fechaVencimiento = user.fechaVencimiento
         });
      }

      return Ok(newObjet);
    }


    [HttpGet]
    public async Task<IActionResult> GetTramites([FromQuery] int? page)
    {
      int _page = page ?? 1;
      int totalRecords = await _context.Nopension.CountAsync();
      int total_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRecords / records)));
      var pensiones = await _context.Nopension.Skip((_page - 1) * records).Take(records).ToListAsync();

      return Ok(
        new
        {
          pages = total_pages,
          records = pensiones,
          total_records = totalRecords,
          current_page = _page
        });
    }

    [HttpGet("test")]
    public async void GetTramitesv2()
    {

      _nopensionService.GenerarCodigoQR("www.google.com", "resources/qr/");

    }


    [HttpPost]
    public async Task<IActionResult> ImportFile([FromForm] ImportFIleDto request)
    {
      var path = _appEnvironment.ContentRootPath;
      var ruta = _config.GetSection("routeImportFile").Value + DateTime.Now.Month + Guid.NewGuid().ToString("N") + ".xlsx";
      var staticPath = Path.Combine(path, ruta);

      FileHelper fileHeader = new FileHelper();
      var validateColumnExcel = fileHeader.ValidateColumns(columnNames, request.Archive);
      if (!validateColumnExcel) return BadRequest(new
      {
        message = "Error, las columnas no coinciden con las del excel: identificacion, nombrecompleto,estado ",
        StatusCode = StatusCodes.Status502BadGateway,
        column = columnNames
      });

      var validateTypeFile = fileHeader.ValidateTypeFile(request.Archive, allowTypes);
      if (!validateTypeFile) return BadRequest(new
      {
        message = "Error, El tipo de archivo no esta permitido",
        StatusCode = StatusCodes.Status405MethodNotAllowed,
      });

      using (var stream = new FileStream(staticPath, FileMode.Create))
      {
        request.Archive.CopyTo(stream);
      }

      var dataExcel = fileHeader.ReadFile(request.Archive);
      fileHeader.deleteFile(staticPath);
    
      await Task.Run(() => _nopensionService.loadUserUsingTask(dataExcel, _config.GetSection("routeQrPath").Value));


      return Ok(new { message = "Se guardo correctamente los usuario" });
    }


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
       
      var header_logo_left = _config.GetSection("routeFileImages").Value + "nopension//logo_depto.png";
      var header_logo_right = _config.GetSection("routeFileImages").Value + "nopension//logo_cesar.png";
      var qr = clienteItem.qrPath;

      //var footer = "ClientApp//src//assets//Imagenes//nopension//footer.png";
      iTextSharp.text.Image img_header_left = iTextSharp.text.Image.GetInstance(header_logo_right);
      iTextSharp.text.Image img_header_right = iTextSharp.text.Image.GetInstance(header_logo_left);
      iTextSharp.text.Image img_user_qr = iTextSharp.text.Image.GetInstance(qr);
      //img_header.ScaleAbsolute(50, 145);
      //img_header.SetAbsolutePosition(7, 50);
      img_header_left.ScaleAbsoluteWidth(95);
      img_header_left.ScaleAbsoluteHeight(95);
      // img_header_left.SetAbsolutePosition(0, 675);
      document.Add(img_header_left);

      img_user_qr.ScaleAbsoluteWidth(95);
      img_user_qr.ScaleAbsoluteHeight(95);
      img_user_qr.SetAbsolutePosition(240, 671);
      document.Add(img_user_qr);

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

     
      clienteItem.updatedAt = DateTime.Now;
      clienteItem.fechaVencimiento = DateTime.Now;
      clienteItem.estadoCertificado = "valido";
      clienteItem.totalDescargas = clienteItem.totalDescargas + 1;
      await Task.Run(() => _nopensionService.updateUser(clienteItem));

      return new FileContentResult(fileContent, "application/pdf");
      //var fileUpload = File(fileStream, "application/octet-stream", "{{filename.ext}}");
      //System.IO.File.Delete(filePath);
      //fileStream.Close();
      //return fileUpload;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUpdateUser(string id, UpdateUserNoPension payload)
    {
      var user = await _context.Nopension.FirstOrDefaultAsync(e => e.Identificacion == id);

      if (user == null)
      {
        return BadRequest(new { message = "Usuario no encontrado" });
      }

      user.Nombrecompleto = payload.Nombrecompleto;
      user.estado = payload.estado;

      _context.Entry(user).State = EntityState.Modified;
      await _context.SaveChangesAsync();

      return Ok(new { message = "Usuario actualizado" });
    }
  }
}