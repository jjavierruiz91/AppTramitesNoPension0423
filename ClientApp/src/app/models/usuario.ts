export class Usuario {
    codusuario!: number;
    id!: string;
    tipoId!: string;
    nombres!: string;
    apellidos!: string;
    correo!: string;
    celular!: string;
    sexo!: string;
    municipio!: string;
    direccion!: string;
    grupoEtnico!: string;
    fechaNacimiento!: string;
    fechaRegistro!: string
    clave!: string;
    rol!: string;
}
export class ForgotPassword {
    Email!: string;
    ClientURI!: string;
    estado!: string;
}

export enum UserRoles {
    Usuario = "Usuario",
    Admin = "Admin",
    FuncionarioHacienda = "FuncionarioHacienda",
    FuncionarioDeporte = "FuncionarioDeporte",
    FuncionarioTIC = "FuncionarioTIC",
    FuncionarioGobiernoTramites = "FuncionarioGobiernoTramites",
    FuncionarioGobierno = "FuncionarioGobierno",
    AsuntosInternos = "AsuntosInternos",
    FuncionarioNoPension = "FuncionarioNoPension"
}


export interface UserNoPension {
    codsolicitante: number,
    estado: string,
    identificacion: string,
    nombrecompleto: string,
}