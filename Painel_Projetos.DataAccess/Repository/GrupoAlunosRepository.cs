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
        #region Context
        dbContext ctx = new dbContext();
        #endregion

        public GrupoAlunosRepository(dbContext context) : base(context)
        {
            ctx = context;
        }

        public GruposAlunos ObterAlunoPor(string nome)
        {
            return ctx.GruposAlunos.Include("Aluno").FirstOrDefault(x => x.Aluno.Nome == nome);
        }

        public GruposAlunos ObterAlunoPor(int idAluno)
        {
            return ctx.GruposAlunos.FirstOrDefault(x => x.AlunoID == idAluno);
        }

        public IList<GruposAlunos> ObterAlunosDoGrupo(int idGrupo)
        {
            return ctx.GruposAlunos.Include("Aluno").Where(x => x.GrupoID == idGrupo).ToList();
        }

        public IList<GruposAlunos> ObterGrupoPorCursoETurma(int cursoID, int turmaID, Periodo periodo)
        {
            return ctx.GruposAlunos.Include("Aluno")
                                    .Include("Grupo")
                                    .Where(x => x.Administrador == true
                                                && x.Aluno.CursoID == cursoID
                                                && x.Aluno.TurmaId == turmaID
                                                && x.Aluno.Periodo == periodo)
                                    .OrderBy(x =>x.Grupo.Nome)
                                    .ToList();
        }

        public IList<GruposAlunos> ObterProprioGrupo(int idAluno)
        {
            return ctx.GruposAlunos.Include("Aluno").Include("Grupo").Where(x => x.Administrador == true && x.AlunoID == idAluno).ToList();
        }

        public new IList<GruposAlunos> ObterTodos()
        {
            return ctx.GruposAlunos.Include("Aluno").Include("Grupo").Where(x => x.Administrador == true).ToList();
        }
    }
}
