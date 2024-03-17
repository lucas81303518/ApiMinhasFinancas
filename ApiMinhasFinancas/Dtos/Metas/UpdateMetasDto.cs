using System.ComponentModel.DataAnnotations;

namespace ApiMinhasFinancas.Dtos.Metas
{
    public class UpdateMetasDto
    {
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Campo Valor é obrigatório!")]
        public double Valor { get; set; }
        public DateTime DataInsercao { get; set; }
        public DateTime DataPrevisao { get; set; }
    }
}
