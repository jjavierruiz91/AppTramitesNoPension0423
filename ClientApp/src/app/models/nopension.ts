export enum estadoPension{
 ///   noDefinido = 'no definido',
    jubilado = 'jubilado',
}

export class Nopension{
    identificacion!: string;
    nombrecompleto!: string;
    estado!: estadoPension;
    isjubilado?: boolean | null;
}