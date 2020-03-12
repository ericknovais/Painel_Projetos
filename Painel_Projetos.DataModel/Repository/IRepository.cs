using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Repository
{
    public interface IRepository
    {
        ICursoRepository Cursos { get; }
        IAlunoRepository Alunos { get; }
        IEmpresaRepository Empresas { get; }
        IRepresentanteRepository Representantes { get; }
        void SaveChanges();
    }
}
