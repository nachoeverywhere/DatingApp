import { Photo } from './photo';

export interface Usuario {
    id: number;
    nombreUsuario: string;
    genero: string;
    conocidoComo: string;
    ciudad: string;
    pais: string;
    edad: number;
    fechaRegistro: Date;
    fechaUltActivo: Date;
    photoUrl: string;
    introduccion?: string;
    intereses?: string;
    buscando?: string;
    fotosPublicas?: Photo[];
}

// Los parametros que son opcionales van al final y llevan '?'.
