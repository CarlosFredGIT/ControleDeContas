using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleContas.Repository.Interfaces
{
    public interface IBaseRepository <T>
    {
        void Inserir(T obj);
        void Alterar(T obj);
        void Excluir(T obj);
        List<T> Consultar();
        T ConsultarPorId(Guid id);
    }
}
