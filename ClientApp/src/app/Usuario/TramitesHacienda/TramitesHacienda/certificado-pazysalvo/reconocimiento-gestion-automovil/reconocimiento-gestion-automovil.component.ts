import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Documento } from 'src/app/models/documento';
import { Stramite } from 'src/app/models/stramite';
import { Tramite } from 'src/app/models/tramite';
import { AuthService } from 'src/app/services/auth.service';
import { DocumentoService } from 'src/app/services/documento.service';
import { StramiteService } from 'src/app/services/stramite.service';
import { TramiteService } from 'src/app/services/tramite.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-reconocimiento-gestion-automovil',
  templateUrl: './reconocimiento-gestion-automovil.component.html',
  styleUrls: ['./reconocimiento-gestion-automovil.component.css']
})
export class ReconocimientoGestionAutomovilComponent implements OnInit {

  constructor(private router: Router, private servicioStramite: StramiteService,
    private servicioDocumentos: DocumentoService, private route: ActivatedRoute,
    private servicioTramites: TramiteService, private authService: AuthService,
    private location: Location) { }

  documentos: Documento[] = [];
  documentosCargados: Documento[] = [];
  documentosListAutomovil: Documento[] = [];
  documento: Documento;
  solicitudTramite: Stramite;
  codeStramite: number;
  solicitudesH: Stramite[] = [];
  codigotramite: string;
  // solicitudesTUsuario: Stramite[] = [];
  tramite: Tramite;
  tipotramite: string;
  ngOnInit() {
    this.tipotramite = this.authService.getTipoTLocalStore();
    this.solicitudTramite = { codstramite: 0, codtramite: 0, codusuario: 0, fecha: "", tipoTramite: "", codFuncionario: 0, estado: "", sectorial: "Hacienda" };
    this.documento = { codocumento: 0, codstramite: 0, observacion: "", fechacreacion: "", fechaactualizacion: "", nombredoc: "", url: "", tamanio: 0, estado: "", plantilla: "", Archive: null }
    this.creacionDocumentosAutomovil();
    this.getAllDocumentos();
    this.codigotramite = this.route.snapshot.paramMap.get('id');
  }

  refresh(): void { window.location.reload(); }

  creacionDocumentosAutomovil() {
    this.servicioStramite.getStramite().subscribe(solic => {
      this.solicitudesH = solic
      var contador = 0;
      var estado = "";
      for (let index = 0; index < this.solicitudesH.length; index++) {
        const element = this.solicitudesH[index];
        if (element.codtramite == Number(this.route.snapshot.paramMap.get('id')) && element.codusuario == this.authService.getCodigoUserLocalStore() && element.tipoTramite == "AUTOMOVIL") {
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
          this.crearDocumentosPaz(res.codstramite);
        }, (err) => {
          console.log(err);
        }
        );
      }
    });
  }
  cancelar()
  {
    this.router.navigate(['TramitesHacienda']);


  }
  goBack(): void {
    this.location.back();
  }

  crearDocumentosPaz(codigoStramite: number) {
    //creamos cada uno de los documentos que nececitamos para el tramite de reconocimiento de personeria
    //Documento Estatutos
  
    this.documento.codstramite = codigoStramite;
    this.documento.observacion = '';
    this.documento.nombredoc = 'TARJETA DE PROPIEDAD';
    this.documento.url = '';
    this.documento.tamanio = 0;
    this.documento.estado = 'Nuevo';
    this.documento.plantilla = '';
    this.documento.fechacreacion = '';
    this.servicioDocumentos.addDocumento(this.documento).subscribe();
    this.getAllDocumentos();
    this.refresh();
  }

  public getAllDocumentos() {
    this.servicioDocumentos.getDocumentos().subscribe(documento => {
      this.documentos = documento
      this.filtarDocumentosListAutomovil();
      this.filtarDocumentosCargadosAutomovil();
    });
  }

  filtarDocumentosListAutomovil() {
    this.documentosListAutomovil = [];
    for (let index = 0; index < this.documentos.length; index++) {
      const element = this.documentos[index];
      if (element.codstramite == this.codeStramite && (element.estado == "Nuevo" || element.estado == "Incorrecto")) {
        this.documentosListAutomovil.push(element);
      }
    }
  }

  filtarDocumentosCargadosAutomovil() {
    for (let index = 0; index < this.documentos.length; index++) {
      const element = this.documentos[index];
      if (element.codstramite == this.codeStramite && element.estado != "Nuevo") {
        this.documentosCargados.push(element);
      }
    }
  }

}
