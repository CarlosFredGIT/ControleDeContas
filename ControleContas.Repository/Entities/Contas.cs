using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleContas.Repository.Entities
{
    public class Contas
    {
        public Guid IdContas { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
