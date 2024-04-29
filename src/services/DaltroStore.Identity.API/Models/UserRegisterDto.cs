using System.ComponentModel.DataAnnotations;

namespace DaltroStore.Identity.API.Models
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(maximumLength: 30, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Compare(nameof(Password),ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword {get; set; } = string.Empty;
    }
}