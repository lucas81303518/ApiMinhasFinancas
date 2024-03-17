using System.ComponentModel.DataAnnotations;

namespace ApiMinhasFinancas.Dtos.TipoContas
{
    public class UpdateTipoContasDto
    {
        [Required(ErrorMessage = "Campo Nome da Conta é obrigatório!")]
        public string NomeConta { get; set; }
        [Required(ErrorMessage = "Campo Tipo da Conta é obrigatório!")]
        public int Tipo { get; set; }
    }
}
