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
    class ProjetosGruposRepository : AbstractRepository<ProjetosGrupos>, IProjetosGruposRepository
    {
        #region Contexto
        dbContext ctx = new dbContext();
        #endregion
        public ProjetosGruposRepository(dbContext context) : base(context)
        {
            ctx = context;
        }

        public ProjetosGrupos ObterPorIdProjeto(int id)
        {
            return ctx.ProjetosGrupos.FirstOrDefault(x => x.ProjetoID == id);
        }

        public List<ProjetosGrupos> ObterProjetoRepresentante(int representanteID)
        {
            return ctx.ProjetosGrupos.Include("Representante").Include("Projeto").Where(x => x.RepresentanteId == representanteID).ToList();
        }

        public new IList<ProjetosGrupos> ObterTodos()
        {
            return  ctx.ProjetosGrupos
                    .Include("Representante")
                    .Include("Projeto")
                    .Include("Empresa")
                    .Where(x => x.GrupoID == null)
                    .OrderBy(i => i.Empresa.RazaoSocial)
                    .ToList();
        }
    }
}
