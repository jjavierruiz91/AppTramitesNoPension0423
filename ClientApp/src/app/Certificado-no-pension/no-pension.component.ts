import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Location } from '@angular/common';
import { estadoPension, Nopension } from '../models/nopension';
import { NopensionService } from '../services/nopension.service';
import { saveAs } from 'file-saver'

@Component({
  selector: 'app-no-pension',
  templateUrl: './no-pension.component.html',
  styleUrls: ['./no-pension.component.css']
})
export class NoPensionComponent implements OnInit {

  registerForm: FormGroup;
  isJubilado: boolean = false;
  buttonDisable: boolean = false;
  submitted
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private NopensionService: NopensionService,
    private toastr: ToastrService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      id: [null, [Validators.required]],
    },

    );

  }

  obtenerFecha() {
    let fechaHoy: Date = new Date();
    return `${fechaHoy.getFullYear()}-${('0' + (fechaHoy.getMonth() + 1)).slice(-2)}-${('0' + (fechaHoy.getDate())).slice(-2)}  ${('0' + (fechaHoy.getHours())).slice(-2)}:${('0' + (fechaHoy.getMinutes())).slice(-2)}:${('0' + (fechaHoy.getSeconds())).slice(-2)}`;;
  }

  get f() { return this.registerForm.controls; }

  onSubmit(form: NgForm) {
    if (!this.registerForm.valid) return this.toastr.warning('se produjo un error, el campo no puede estar vacio')
    const data = this.registerForm.getRawValue();
    this.buttonDisable = true;
    this.isJubilado = false;
    this.NopensionService.get(data.id).subscribe((pension) => {
      this.buttonDisable = false;
      const blob = new Blob([pension], { type: 'application/pdf' });
      const url = window.URL.createObjectURL(blob);
      saveAs(url, "Certificado NoPension")
    }, error => {
      this.buttonDisable = false;
      console.log("erro", error);
      let message = "Ocurrio un error al descargar el pdf, contactese con el administrador"

      if (error.status === 401) {
        message = "El usuario que intenta buscar no fue encontrado"
      }

      if (error.status === 402) {
        message = "El usuario registrado se encuentra Jubilado"
      }
      return this.toastr.warning(message)
    })

  }

  goBack(): void {
    this.location.back();
  }

  onReset() {
    this.submitted = false;
    this.registerForm.reset();
    this.goBack();
  }

}

