using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMinhasFinancas.Models
{
    public class Documentos
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Numero do Documento é obrigatório!")]
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

        [ForeignKey("FormasPgtoDB")]      
        public FormasPagamento Fk_FormaPgto { get; set; }
      
        [ForeignKey("TipoContasDB")]     
        public TipoContas Fk_Tipoconta { get; set; } 
        
        [ForeignKey("UsuariosDB")]    
        public Usuarios Fk_usuario { get; set; } 
    }
}
