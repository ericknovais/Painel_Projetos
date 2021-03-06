﻿using AutenticacaoNoAspNetMVC.Filters;
using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using Painel_Projetos.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Painel_Projetos.Web.Controllers
{
    public class GrupoController : Controller
    {
        #region Repository
        Repository repository = new Repository();
        #endregion
        // GET: Grupo
        public ActionResult List()
        {
            IList<GruposAlunos> lista = new List<GruposAlunos>();
            try
            {
                lista = repository.GruposAlunos.ObterTodos();
                return View(lista);
            }
            catch (Exception ex)
            {
                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
                return View(lista);
            }

        }

        [AutorizacaoTipo(new[] { Perfil.Aluno })]
        public ActionResult Edit(int id = 0)
        {
            GrupoViewModel viewModel = new GrupoViewModel();
            var aluno = repository.GruposAlunos.ObterAlunoPor(User.Identity.Name);
            if (aluno != null && aluno.ID != id)
            {
                TempData["Alerta"] = "Você já esta em um grupo, por isso não pode criar outro!";
                return RedirectToAction("List");
            }
            try
            {
                GruposAlunos gruposAlunos = id.Equals(0) ? new GruposAlunos() : repository.GruposAlunos.ObterPor(id);
                Grupo grupo = gruposAlunos.GrupoID.Equals(0) ? new Grupo() : repository.Grupo.ObterPor(gruposAlunos.GrupoID);
                viewModel.NomeAluno = User.Identity.Name;
                viewModel.NomeGrupo = grupo.Nome;
                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
                return View(viewModel);
            }

        }

        [HttpPost]
        public ActionResult Edit(GrupoViewModel viewModel, int id = 0)
        {
            try
            {
                GruposAlunos gruposAlunos = id.Equals(0) ? new GruposAlunos() : repository.GruposAlunos.ObterPor(id);
                Grupo grupo = id.Equals(0) ? new Grupo() : repository.Grupo.ObterPor(gruposAlunos.GrupoID);

                Aluno aluno = repository.Aluno.ObterPor(User.Identity.Name);

                grupo.Nome = viewModel.NomeGrupo;
                repository.Grupo.Salvar(grupo);

                gruposAlunos.Grupo = grupo;
                gruposAlunos.Aluno = aluno;
                gruposAlunos.Administrador = true;

                repository.GruposAlunos.Salvar(gruposAlunos);
                repository.SaveChanges();

                if (id.Equals(0))
                {
                    TempData["Sucesso"] = "Grupo criado com sucesso";
                }

                TempData["Sucesso"] = "Grupo alterado com sucesso";

                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
                return View(viewModel);
            }
        }
    }
}