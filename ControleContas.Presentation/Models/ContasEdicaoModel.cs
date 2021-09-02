using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleContas.Presentation.Models
{
    public class ContasEdicaoModel
    {
        public Guid IdContas { get; set; }

        [MinLength(3, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome da conta.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o preço da conta.")]
        public string Preco { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de vencimento da conta.")]
        public string DataVencimento { get; set; }
    }
}
