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
        ILoginRepository Login { get; }
        void SaveChanges();
    }
}
