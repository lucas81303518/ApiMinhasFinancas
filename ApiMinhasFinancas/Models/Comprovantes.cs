using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMinhasFinancas.Models
{
    public class Comprovantes
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Titulo é obrigatório!")]        
        public string TituloArquivo { get; set; }
        [Required(ErrorMessage = "Campo Caminho é obrigatório!")]        
        public string CaminhoArquivo { get; set; }
        [Required(ErrorMessage = "Campo Tipo do Comprovante é obrigatório!")]        
        public string TipoComprovante { get; set; }
        [ForeignKey("Id")]
        public Documentos FK_Documento { get; set; }
    }
}
