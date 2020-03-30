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
    class UsuarioRepository : AbstractRepository<Usuario>, IUsuarioRepository
    {
        dbContext ctx = new dbContext();

        public UsuarioRepository(dbContext context) : base(context)
        {
            this.ctx = context;
        }

        public Usuario ObterAluno(int idAluno)
        {
            return ctx.Usuarios.FirstOrDefault(x => x.AlunoID == idAluno); ;
        }

        public Usuario ObterRepresentante(int idRepresentante)
        {
            return ctx.Usuarios.FirstOrDefault(x => x.RepresentanteID == idRepresentante);
        }

        public Usuario ObterSenhaPor(string login)
        {
            return ctx.Usuarios.FirstOrDefault(x => x.Login == login);
        }
    }
}
