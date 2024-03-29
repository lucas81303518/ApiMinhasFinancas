﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMinhasFinancas.Models
{
   
    public class TipoContas
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Nome da Conta é obrigatório!")]
        public string NomeConta { get; set; }
        [Required(ErrorMessage = "Campo Tipo da Conta é obrigatório!")]
        public int Tipo { get; set; }       
    }
}
