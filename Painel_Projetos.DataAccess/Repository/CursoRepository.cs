using Painel_Projetos.DataAccess.contextDB;
using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using Painel_Projetos.DomainModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DataAccess.Repository
{
    class CursoRepository : AbstractRepository<Curso>, ICursoRepository
    {
        #region Context
        dbContext ctx = new dbContext();
        #endregion

        public CursoRepository(dbContext context) : base(context)
        {
            ctx = context;
        }

        public IList<Curso> ObterCursoAtivo()
        {
            return this.ctx.Cursos.Where(x => x.Ativo.Equals(true)).ToList();
        }
    }
}
