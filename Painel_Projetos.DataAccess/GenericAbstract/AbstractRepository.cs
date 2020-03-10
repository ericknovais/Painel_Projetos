using Painel_Projetos.DataAccess.contextDB;
using Painel_Projetos.DomainModel.Class;
using Painel_Projetos.DomainModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DataAccess.GenericAbstract
{
    abstract class AbstractRepository<T> : IRepositoryBase<T> where T : EntityBase
    {
        #region dbContext
        dbContext ctx;
        #endregion

        public AbstractRepository(dbContext context)
        {
            ctx = context;
        }

        public void Excluir(T entity)
        {
            ctx.Set<T>().Remove(entity);
        }

        public T ObterPor(int id)
        {
            return ctx.Set<T>().FirstOrDefault(x => x.ID.Equals(id));
        }

        public IList<T> ObterTodos()
        {
           return ctx.Set<T>().ToList();
        }

        public void Salvar(T entity)
        {
            if (entity.ID.Equals(0))
            {
                ctx.Set<T>().Add(entity);
            }
        }
    }
}
