using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Painel_Projetos.Web.Controllers
{
    public class TurmaController : Controller
    {
        #region repositoy
        Repository repository = new Repository();
        #endregion

        // GET: Turma
        public ActionResult List()
        {
            IList<Turma> lista = new List<Turma>();
            try
            {
                lista = repository.Turma.ObterTodos();

                if (lista.Count.Equals(0))
                    TempData["ListaVazia"] = "Ainda não tem turmas cadastradas";
            }
            catch (Exception ex)
            {
                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
            }
            return View(lista);
        }

        public ActionResult Edit(int id = 0)
        {
            Turma turma = new Turma();
            try
            {
                turma = id.Equals(0) ? new Turma() : repository.Turma.ObterPor(id);
            }
            catch (Exception ex)
            {
                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
            }
            return View(turma);
        }

        [HttpPost]
        public ActionResult Edit(Turma entity, int id = 0)
        {
            Turma turma = new Turma();
            try
            {
                turma = id.Equals(0) ? new Turma() : repository.Turma.ObterPor(id);
                turma.Descricao = entity.Descricao;
                repository.Turma.Salvar(turma);
                repository.SaveChanges();

                if (id.Equals(0))
                {
                    ModelState.Clear();
                    TempData["Mensagem"] = "Sucesso";
                    return View(new Turma());
                }
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
            }
            return View(turma);
        }
    }
}