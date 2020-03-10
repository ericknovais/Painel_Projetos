using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Repository
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        void Salvar(T entity);
        void Excluir(T entity);
        T ObterPor(int id);
        IList<T> ObterTodos();
    }
}
