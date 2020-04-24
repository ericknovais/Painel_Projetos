using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Repository
{
    public interface IRepository
    {
        ICursoRepository Curso { get; }
        IAlunoRepository Aluno { get; }
        IEmpresaRepository Empresa { get; }
        IRepresentanteRepository Representante { get; }
        ICordenadorRepository Cordenador { get; }
        IUsuarioRepository Usuario { get; }
        IGrupoRepository Grupo { get; }
        IGruposAlunosRepository GruposAlunos { get; }
        IProjetoRepository Projeto { get; }
        IProjetosGruposRepository ProjetosGrupos { get; }
        void SaveChanges();
    }
}
