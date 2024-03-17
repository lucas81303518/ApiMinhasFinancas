using System.ComponentModel.DataAnnotations;

namespace ApiMinhasFinancas.Login
{
    public class CredenciaisLogin
    {
        [Required(ErrorMessage = "Usuário é obrigatorio!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatorio!")]
        public string Senha { get; set; }
    }

}
