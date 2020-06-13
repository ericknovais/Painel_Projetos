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
    class TurmaRepository : AbstractRepository<Turma>, ITurmarepository
    {
        dbContext ctx = new dbContext();
        public TurmaRepository(dbContext context) : base(context)
        {
            ctx = context;
        }

        public new IList<Turma> ObterTodos()
        {
            return ctx.Turmas.OrderBy(x => x.Descricao).ToList();
        }
    }
}
