using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiMinhasFinancas.Models;

namespace ApiMinhasFinancas.Dtos.Documentos
{
    public class ReadSaldoFormasPagamentoDto
    {       
        public double ValorTotal { get; set; }            
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
