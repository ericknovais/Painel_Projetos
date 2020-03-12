﻿using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Painel_Projetos.Web.Controllers
{
    public class EmpresaController : Controller
    {
        #region Repository
        Repository repository = new Repository();
        #endregion

        // GET: Empresa
        public ActionResult List()
        {
            IList<Empresa> empresas = new List<Empresa>();
            try
            {
                empresas = repository.Empresas.ObterTodos();
                return View(empresas);
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "<\br>");
                return View(empresas);
            }
        }

        public ActionResult Edit(int id = 0)
        {
            Empresa empresa = new Empresa();
            Representante representante = new Representante();
            try
            {
                empresa = id.Equals(0) ? new Empresa() : repository.Empresas.ObterPor(id);
                representante = empresa.RepresentanteId.Equals(0) ? new Representante() : repository.Representantes.ObterPor(empresa.RepresentanteId);
                return View(empresa);
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "<\br>");
                return View(empresa);
            }
        }

        [HttpPost]
        public ActionResult Edit(Empresa entity, int id = 0)
        {
            Empresa empresa = new Empresa();
            Representante representante = new Representante();
            try
            {
                empresa = id.Equals(0) ? new Empresa() : repository.Empresas.ObterPor(id);
                representante = empresa.RepresentanteId.Equals(0) ? new Representante() : repository.Representantes.ObterPor(empresa.RepresentanteId);
                empresa.RazaoSocial = entity.RazaoSocial;
                empresa.CNPJ = entity.CNPJ;
                representante.Nome = entity.Representante.Nome;
                representante.Email = entity.Representante.Email;
                repository.Representantes.Salvar(representante);
                repository.Empresas.Salvar(empresa);
                repository.SaveChanges();
                if (id.Equals(0))
                {
                    ModelState.Clear();
                    Representante rep = new Representante();
                    return View(new Empresa(rep));
                }
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {

                return View();
            }
        }
    }
}