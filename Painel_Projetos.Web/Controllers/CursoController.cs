using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Painel_Projetos.Web.Controllers
{
    public class CursoController : Controller
    {
        #region Repository
        Repository repository = new Repository();
        #endregion

        // GET: Curso
        public ActionResult List()
        {
            IList<Curso> cursos = new List<Curso>();
            try
            {
                cursos = repository.Cursos.ObterTodos();
                return View(cursos);
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "</br>");
                return View(cursos);
            }
        }

        public ActionResult Edit(int id = 0)
        {
            Curso curso = new Curso();
            try
            {
                curso = id.Equals(0) ? new Curso() : repository.Cursos.ObterPor(id);
                return View(curso);
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "</br>");
                return View(curso);
            }
        }

        [HttpPost]
        public ActionResult Edit(Curso entity, int id = 0)
        {
            Curso curso = new Curso();
            try
            {
                curso = id.Equals(0) ? new Curso() : repository.Cursos.ObterPor(id);
                curso.Descricao = entity.Descricao;
                curso.Ativo = entity.Ativo;
                curso.Validar();
                repository.Cursos.Salvar(curso);
                repository.SaveChanges();
                ViewBag.Mensagem = "O curso foi salvo com sucesso!";
                if (id.Equals(0))
                {
                    ModelState.Clear();
                    return View(new Curso());
                }
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "</br>");
                return View();
            }
        }
    }
}