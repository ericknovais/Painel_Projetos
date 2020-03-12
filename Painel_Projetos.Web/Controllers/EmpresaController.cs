using Painel_Projetos.DataAccess.GenericAbstract;
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
        Repository rep = new Repository();
        #endregion

        // GET: Empresa
        public ActionResult List()
        {
            IList<Empresa> empresas = new List<Empresa>(); 
            try
            {
                empresas = rep.Empresas.ObterTodos();
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
            try
            {
                empresa = id.Equals(id) ? new Empresa() : rep.Empresas.ObterPor(id);
                return View(empresa);
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "<\br>");
                return View(empresa);
            }
        }

        [HttpPost]
        public ActionResult Edit(Empresa entity, int id =0)
        {
            return View();
        }
    }
}