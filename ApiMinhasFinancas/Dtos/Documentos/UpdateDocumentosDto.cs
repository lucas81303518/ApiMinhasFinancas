using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApiMinhasFinancas.Dtos.TipoContas;
using ApiMinhasFinancas.Dtos.Usuarios;
using ApiMinhasFinancas.Dtos.FormasPagamento;

namespace ApiMinhasFinancas.Dtos.Documentos
{
    public class UpdateDocumentosDto
    {
        public string NumeroDocumento { get; set; }
        [Required(ErrorMessage = "Campo Numero do Descrição é obrigatório!")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Campo Valor do Documento é obrigatório!")]
        public double Valor { get; set; }
        [Required(ErrorMessage = "Campo Data do Documento é obrigatório!")]
        public DateTime DataDocumento { get; set; }
        public int QtdParcelas { get; set; }
        [Required(ErrorMessage = "Campo Status do Documento é obrigatório!")]
        public string Status { get; set; }
        public int CodigoMeta { get; set; }

        [ForeignKey("Id")]
        public UpdateTipoContasDto Fk_Tipoconta { get; set; }
        [ForeignKey("Id")]
        public UpdateUsuarioDto Fk_usuario { get; set; }
        [ForeignKey("Id")]
        public UpdateFormasPagamentoDto Fk_FormaPgto { get; set; }
    }
}
