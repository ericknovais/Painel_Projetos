using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Repository
{
    public interface IAlunoRepository : IRepositoryBase<Aluno>
    {
        Aluno ObterPor(string nome);
        IList<Aluno> ObterAlunosTurma(int curso, int turma);

        IList<Aluno> ObterAlunosQueNaoEstaoEmGrupo(int curso, int turma, Periodo periodo);
    }
}
