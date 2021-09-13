using ControleContas.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleContas.Repository.Interfaces
{
    public interface IContasRepository : IBaseRepository<Contas>
    {
        List<Contas> ConsultarPorDatas(DateTime dataMin, DateTime dataMax);

       // List<Contas> VerificaDataVencimento(DateTime dataVencimento);
    }
}
