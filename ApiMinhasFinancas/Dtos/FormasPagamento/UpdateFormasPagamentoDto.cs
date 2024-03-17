using System.ComponentModel.DataAnnotations;

namespace ApiMinhasFinancas.Dtos.FormasPagamento
{
    public class UpdateFormasPagamentoDto
    {
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Valor é obrigatório!")]
        public double Valor { get; set; }
    }
}
