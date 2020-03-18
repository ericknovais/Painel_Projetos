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
    class LoginRepository : AbstractRepository<Login>, ILoginRepository
    {
        dbContext ctx = new dbContext();

        public LoginRepository(dbContext context) : base(context)
        {
            this.ctx = context;
        }

        public Login ObterAluno(int? idAluno)
        {
            return ctx.Logins.Where(x => x.AlunoId == idAluno).First(); ;
        }
    }
}
