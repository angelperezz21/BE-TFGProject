﻿namespace TFGProject.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string CIF { get; set; }
        public string Contrasenya { get; set; }
        public string? Descripcion { get; set; }
        public string Nombre { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
        public string Web { get; set; }
        public string Contacto { get; set; }
        public string Categoria { get; set; }
        public string? imgUrl { get; set; }
        public string PasswordSinHash { get; set; }
        public int? Notificacion { get; set; }


    }
}
