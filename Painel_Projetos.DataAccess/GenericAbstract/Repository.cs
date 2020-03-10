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

        ICursoRepository cursos;
        public ICursoRepository Cursos
        {
            get
            {
                return cursos ?? (cursos = new CursoRepository(ctx));
            }
        }

        IAlunoRepository alunos;
        public IAlunoRepository Alunos
        {
            get
            {
                return alunos ?? (alunos = new AlunoRepository(ctx));
            }
        }

        public void SaveChanges()
        {
            ctx.SaveChanges();
        }
    }
}
