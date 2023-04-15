import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Documento } from 'src/app/models/documento';
import { Stramite } from 'src/app/models/stramite';
import { Tramite, } from 'src/app/models/tramite';
import { AuthService } from 'src/app/services/auth.service';
import { DocumentoService } from 'src/app/services/documento.service';
import { StramiteService } from 'src/app/services/stramite.service';

@Component({
  selector: 'app-inscripcion-gestion-dignatarios',
  templateUrl: './inscripcion-gestion-dignatarios.component.html',
  styleUrls: ['./inscripcion-gestion-dignatarios.component.css']
})
export class InscripcionGestionDignatariosComponent implements OnInit {


  constructor(
    private servicioStramite: StramiteService, private servicioDocumentos: DocumentoService,
    private route: ActivatedRoute, private authService: AuthService, private router: Router) { }

  documentos: Documento[] = [];
  documentosCargados: Documento[] = [];
  documentosList: Documento[] = [];
  documento: Documento;
  solicitudTramite: Stramite;
  codeStramite: number;
  solicitudesT: Stramite[] = [];
  codigotramite: string;
  tramite: Tramite;
  color: string;
  tipotramite: string;
  ngOnInit() {
    this.tipotramite = this.authService.getTipoTLocalStore();
    this.solicitudTramite = { codstramite: 0, codtramite: 0, codusuario: 0, fecha: "", tipoTramite: "", codFuncionario: 0, estado: "", sectorial: "Gobierno" };
    this.documento = { codocumento: 0, codstramite: 0, observacion: "", fechacreacion: "", fechaactualizacion: "", nombredoc: "", url: "", tamanio: 0, estado: "", plantilla: "", Archive: null }
    this.creacionDocumentosLigas();
    this.getAllDocumentos();
    this.codigotramite = this.route.snapshot.paramMap.get('id');
  }
  cancelar()
  {
    this.router.navigate(['TramitesGobierno']);

  }

  refresh(): void { window.location.reload(); }

  creacionDocumentosLigas() {
    this.servicioStramite.getStramite().subscribe(solic => {
      this.solicitudesT = solic
      var contador = 0;
      var estado = "";
      if (localStorage.getItem('tipoTramite') == "PRIMER GRADO Y SEGUNDO GRADO") {
        for (let index = 0; index < this.solicitudesT.length; index++) {
          const element = this.solicitudesT[index];
          if (element.codtramite == Number(this.route.snapshot.paramMap.get('id')) && element.codusuario == this.authService.getCodigoUserLocalStore() && element.tipoTramite == "PRIMER GRADO Y SEGUNDO GRADO") {
            contador++;
            this.codeStramite = element.codstramite;
            if(element.estado == "FINALIZADO")
            {
              estado = "OK";
            }
            if(element.estado == "EN PROCESO")
            {
              estado = "";
            }
          }
        }
        if (contador == 0 || estado == "OK") {
          this.solicitudTramite.codtramite = Number(this.route.snapshot.paramMap.get('id'));
          this.solicitudTramite.codusuario = this.authService.getCodigoUserLocalStore();
          this.solicitudTramite.tipoTramite = this.authService.getTipoTLocalStore();
          this.solicitudTramite.estado = "EN PROCESO";
          this.servicioStramite.addStramite(this.solicitudTramite).subscribe(res => {
            localStorage.setItem('codigosolicitud', res.codstramite.toString());
            this.crearDocumentosTramiteInscripcion(res.codstramite);
          }, (err) => {
            console.log(err);
          }
          );
        }
      } else if (localStorage.getItem('tipoTramite') == " ") {
        for (let index = 0; index < this.solicitudesT.length; index++) {
          const element = this.solicitudesT[index];
          if (element.codtramite == Number(this.route.snapshot.paramMap.get('id')) && element.codusuario == this.authService.getCodigoUserLocalStore() && element.tipoTramite == "  ") {
            contador++;
            this.codeStramite = element.codstramite;
            if(element.estado == "FINALIZADO")
            {
              estado = "OK";
            }
            if(element.estado == "EN PROCESO")
            {
              estado = "";
            }
          }
        }
        if (contador == 0 || estado == "OK") {
          this.solicitudTramite.codtramite = Number(this.route.snapshot.paramMap.get('id'));
          this.solicitudTramite.codusuario = this.authService.getCodigoUserLocalStore();
          this.solicitudTramite.tipoTramite = this.authService.getTipoTLocalStore();
          this.servicioStramite.addStramite(this.solicitudTramite).subscribe(res => {
            localStorage.setItem('codigosolicitud', res.codstramite.toString());
            this.crearDocumentosTramiteInscripcion(res.codstramite);
          }, (err) => {
            console.log(err);
          }
          );
        }
      }
    });
  }

  crearDocumentosTramiteInscripcion(codigoStramite: number) {
    //creamos cada uno de los documentos que nececitamos para el tramite de INSCRIPCIÓN DE DIGNATARIOS DE LOS ORGANISMOS DEPORTIVOS Y RECREATIVOS
    //Documento Actas de eleccion de los miembros
    this.documento.codstramite = codigoStramite;
    this.documento.observacion = '';
    this.documento.nombredoc = 'INSCRIPCIÓN DE DIGNATARIOS';
    this.documento.url = '';
    this.documento.tamanio = 0;
    this.documento.estado = 'Nuevo';
    this.documento.plantilla = 'INSCRIPCIÓN_DE_DIGNATARIOS_plantilla.docx';
    this.documento.fechacreacion = '';
    this.servicioDocumentos.addDocumento(this.documento).subscribe();
    //Documento Certificado de idoneidad
    this.documento.codstramite = codigoStramite;
    this.documento.observacion = '';
    this.documento.nombredoc = 'ACTA DE ELECCIÓN';
    this.documento.url = '';
    this.documento.tamanio = 0;
    this.documento.estado = 'Nuevo';
    this.documento.plantilla = ' ';
    this.documento.fechacreacion = '';
    this.servicioDocumentos.addDocumento(this.documento).subscribe();
    //Documento Carta de aceptación de cargos
    this.documento.codstramite = codigoStramite;
    this.documento.observacion = '';
    this.documento.nombredoc = 'LISTADO DE VOTANTES';
    this.documento.url = '';
    this.documento.tamanio = 0;
    this.documento.estado = 'Nuevo';
    this.documento.plantilla = ' ';
    this.documento.fechacreacion = '';
    this.servicioDocumentos.addDocumento(this.documento).subscribe();
    //Documento Copia Reconocimiento Deportivo
    this.documento.codstramite = codigoStramite;
    this.documento.observacion = '';
    this.documento.nombredoc = 'ACTA DE CONSTITUCIÓN DE TRIBUNAL DE GARANTÍAS';
    this.documento.url = '';
    this.documento.tamanio = 0;
    this.documento.estado = 'Nuevo';
    this.documento.plantilla = ' ';
    this.documento.fechacreacion = '';
    this.servicioDocumentos.addDocumento(this.documento).subscribe();
    this.getAllDocumentos();
    this.refresh();

    //
    this.documento.codstramite = codigoStramite;
    this.documento.observacion = '';
    this.documento.nombredoc = 'LISTADO DE ASISTENCIA DEL TRIBUNAL DE GARANTÍAS';
    this.documento.url = '';
    this.documento.tamanio = 0;
    this.documento.estado = 'Nuevo';
    this.documento.plantilla = ' ';
    this.documento.fechacreacion = ' ';
    this.servicioDocumentos.addDocumento(this.documento).subscribe();
    this.getAllDocumentos();
    this.refresh();

    this.documento.codstramite = codigoStramite;
    this.documento.observacion = '';
    this.documento.nombredoc = 'PLANCHAS INSCRITAS';
    this.documento.url = '';
    this.documento.tamanio = 0;
    this.documento.estado = 'Nuevo';
    this.documento.plantilla = ' ';
    this.documento.fechacreacion = ' ';
    this.servicioDocumentos.addDocumento(this.documento).subscribe();
    this.getAllDocumentos();
    this.refresh();
  }

  getAllDocumentos() {
    this.servicioDocumentos.getDocumentos().subscribe(documento => {
      this.documentos = documento
      this.filtarDocumentosListInscripcion();
      this.filtarDocumentosCargadosInscripcion();
    });
  }

  filtarDocumentosListInscripcion() {
    this.documentosList = [];
    for (let index = 0; index < this.documentos.length; index++) {
      const element = this.documentos[index];
      if (element.codstramite == this.codeStramite && (element.estado == "Nuevo" || element.estado == "Incorrecto")) {
        this.documentosList.push(element);
      }
    }
  }

  filtarDocumentosCargadosInscripcion() {
    for (let index = 0; index < this.documentos.length; index++) {
      const element = this.documentos[index];
      if (element.codstramite == this.codeStramite && element.estado != "Nuevo") {
        this.documentosCargados.push(element);
      }
    }
  }
}
