using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleContas.Presentation.Models
{
    public class AlterarSenhaModel
    {
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a senha atual.")]
        public string SenhaAtual { get; set; }

        [StrongPassword(ErrorMessage = "Informe pelo menos 1 letra minuscula, 1 letra maiuscula e 1 digito numérico.")]
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a nova senha.")]
        public string NovaSenha { get; set; }

        [Compare("NovaSenha", ErrorMessage = "Senhas não conferem.")]
        [Required(ErrorMessage = "Por favor confirme a nova senha.")]
        public string NovaSenhaConfirmacao { get; set; }
    }
}
