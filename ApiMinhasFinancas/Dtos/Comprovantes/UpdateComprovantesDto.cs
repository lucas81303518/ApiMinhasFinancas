using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApiMinhasFinancas.Dtos.Documentos;

namespace ApiMinhasFinancas.Dtos.Comprovantes
{
    public class UpdateComprovantesDto
    {
        public string TituloArquivo { get; set; }
        [Required(ErrorMessage = "Campo Caminho é obrigatório!")]
        public string CaminhoArquivo { get; set; }
        [Required(ErrorMessage = "Campo Tipo do Comprovante é obrigatório!")]
        public string TipoComprovante { get; set; }
        [ForeignKey("Id")]
        public UpdateDocumentosDto FK_Documento { get; set; }
    }
}
