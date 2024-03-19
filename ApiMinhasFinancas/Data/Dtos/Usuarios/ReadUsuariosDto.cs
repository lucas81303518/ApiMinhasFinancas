﻿using ApiMinhasFinancas.Dtos.Documentos;
using System.ComponentModel.DataAnnotations;

namespace ApiMinhasFinancas.Dtos.Usuarios
{
    public class ReadUsuariosDto
    {
        public int Id { get; set; }        
        public string Nome { get; set; }        
        public string Email { get; set; }        
        public string Senha { get; set; }      
    }
}
