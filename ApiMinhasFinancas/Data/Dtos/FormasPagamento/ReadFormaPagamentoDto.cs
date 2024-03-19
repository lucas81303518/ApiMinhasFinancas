using ApiMinhasFinancas.Dtos.Documentos;
using System.ComponentModel.DataAnnotations;

namespace ApiMinhasFinancas.Dtos.FormasPagamento
{
    public class ReadFormaPagamentoDto
    {
        public int Id { get; set; }        
        public string Nome { get; set; }        
        public double Valor { get; set; }        
    }
}
