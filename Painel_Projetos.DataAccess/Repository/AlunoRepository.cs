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
    class AlunoRepository : AbstractRepository<Aluno>, IAlunoRepository
    {
        dbContext ctx = new dbContext();
        public AlunoRepository(dbContext context) : base(context)
        {
            ctx = context;
        }

        public IList<Aluno> ObterAlunosQueNaoEstaoEmGrupo(int curso, int turma, Periodo periodo)
        {
            var alunos = from aluno in ctx.Alunos
                        join aluGp in ctx.GruposAlunos on aluno.ID equals aluGp.AlunoID into aluGp
                        from alunoGrupo in aluGp.DefaultIfEmpty()
                        where   aluno.CursoID.Equals(curso)
                                & aluno.TurmaId.Equals(turma)
                                & aluno.Periodo == periodo
                                & alunoGrupo.AlunoID.Equals(null)  
                        select aluno;

            return alunos.ToList();
        }

        public IList<Aluno> ObterAlunosTurma(int curso, int turma)
        {
            return ctx.Alunos.Include("Curso").Include("Turma").Where(x =>x.CursoID == curso && x.TurmaId == turma).OrderBy(x => x.Nome).ToList();
        }

     

        public Aluno ObterPor(string nome)
        {
            return ctx.Alunos.FirstOrDefault(x => x.Nome == nome);
        }

        public new IList<Aluno> ObterTodos()
        {
            return ctx.Alunos.Include("Curso").Include("Turma").OrderBy(x => x.Nome).ToList();
        }
    }
}
