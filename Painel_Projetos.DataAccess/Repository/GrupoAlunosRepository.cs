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
    class GrupoAlunosRepository : AbstractRepository<GruposAlunos>, IGruposAlunosRepository
    {
        public GrupoAlunosRepository(dbContext context) : base(context)
        {
        }
    }
}
