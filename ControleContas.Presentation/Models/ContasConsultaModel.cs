using ControleContas.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleContas.Presentation.Models
{
    public class ContasConsultaModel
    {
        [Required(ErrorMessage = "Por favor, informar a data de início.")]
        public string DataMin { get; set; }

        [Required(ErrorMessage = "Por favor, informar a data de término.")]
        public string DataMax { get; set; }

        public List<Contas> Contas { get; set; }
    }
}
