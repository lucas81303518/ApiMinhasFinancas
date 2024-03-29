﻿using System.ComponentModel.DataAnnotations;

namespace ApiMinhasFinancas.Dtos.Metas
{
    public class UpdateMetasDto
    {
        public string Descricao { get; set; }   
        public double Valor { get; set; }
        public DateTimeOffset DataInsercao { get; set; }
        public DateTimeOffset DataPrevisao { get; set; }
    }
}
