using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleContas.Presentation.Models
{
    public class AccountRegisterModel
    {
        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email.")]
        public string Email { get; set; }

        [StrongPassword(ErrorMessage = "Informe pelo menos 1 letra minuscula, 1 letra maiuscula e 1 digito numérico.")]
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a senha.")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem.")]
        [Required(ErrorMessage = "Por favor, confirme a senha.")]
        public string SenhaConfirmacao { get; set; }
    }

    //classe para validação customizada do campo senha
    public class StrongPassword : ValidationAttribute
    {
        //value -> contem o valor do campo que será validado
        public override bool IsValid(object value)
        {
            if(value != null)
            {
                var senha = value.ToString();

                return senha.Any(char.IsUpper)  //pelo menos 1 letra maiuscula
                    && senha.Any(char.IsLower)  //pelo menos 1 letra minuscula 
                    && senha.Any(char.IsDigit); //pelo menos 1 digito numerico
            }

            return false;
        }
    }
}
