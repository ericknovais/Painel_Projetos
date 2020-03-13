using Painel_Projetos.DataAccess.contextDB;
using Painel_Projetos.DataAccess.Repository;
using Painel_Projetos.DomainModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DataAccess.GenericAbstract
{
    public class Repository : IRepository
    {
        #region Context
        dbContext ctx;
        #endregion

        #region Construtor
        public Repository()
        {
            ctx = new dbContext();
        }
        #endregion

        ICursoRepository curso;
        public ICursoRepository Curso
        {
            get
            {
                return curso ?? (curso = new CursoRepository(ctx));
            }
        }

        IAlunoRepository aluno;
        public IAlunoRepository Aluno
        {
            get
            {
                return aluno ?? (aluno = new AlunoRepository(ctx));
            }
        }

        IEmpresaRepository empresa;
        public IEmpresaRepository Empresa
        {
            get
            {
                return empresa ?? (empresa = new EmpresaRepository(ctx));
            }
        }

        IRepresentanteRepository representante;
        public IRepresentanteRepository Representante
        {
            get
            {
                return representante ?? (representante = new RepresentanteRepository(ctx));
            }
        }

        ILoginRepository login;
        public ILoginRepository Login
        {
            get
            {
                return login ?? (login = new LoginRepository(ctx));
            }
        }

        public void SaveChanges()
        {
            ctx.SaveChanges();
        }
    }
}
