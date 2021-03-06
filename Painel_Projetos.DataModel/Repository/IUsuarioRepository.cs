﻿using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Repository
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Usuario ObterAluno(int idAluno);
        Usuario ObterRepresentante(int idRepresentante);
        Usuario ObterCoordenador(int idCoordenador);
        Usuario ObterPeloLogin(string login);
    }
}
