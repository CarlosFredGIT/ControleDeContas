using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleContas.Messages.Models
{
    public class EmailModel
    {
        public string EmailDestinatario { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
    }
}
