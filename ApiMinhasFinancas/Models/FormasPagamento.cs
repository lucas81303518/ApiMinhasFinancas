using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMinhasFinancas.Models
{
    public class FormasPagamento
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Nome é obrigatório!")]        
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Valor é obrigatório!")]        
        public double Valor { get; set; }
        public virtual Documentos Documento { get; set; }
    }
}
