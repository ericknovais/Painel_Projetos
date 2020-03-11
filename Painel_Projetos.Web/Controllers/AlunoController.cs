using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Painel_Projetos.Web.Controllers
{
    public class AlunoController : Controller
    {
        #region Repository
        Repository repository = new Repository();
        #endregion

        // GET: Aluno
        public ActionResult List()
        {
            IList<Aluno> lista = new List<Aluno>();
            try
            {
                lista = repository.Alunos.ObterTodos();
                ViewBag.CursoID = new SelectList(
                    repository.Cursos.ObterTodos(),
                    "Id",
                    "Descricao"
                );
                return View(lista);
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "</br>");
                return View(lista);
            }
        }

        public ActionResult Edit(int id = 0)
        {
            Aluno entity = new Aluno();
            try
            {
                entity = id.Equals(0) ? new Aluno() : repository.Alunos.ObterPor(id);

                ViewBag.CursoId = new SelectList
                    (
                        repository.Cursos.ObterCursoAtivo(),
                        "Id",
                        "Descricao",
                        entity.CursoID
                    );

                return View(entity);
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "</br>");
                return View(entity);
            }
        }

        [HttpPost]
        public ActionResult Edit(Aluno entity, int id = 0)
        {
            Aluno aluno = new Aluno();
            try
            {
                aluno = id.Equals(0) ? new Aluno() : repository.Alunos.ObterPor(id);
                aluno.ID = entity.ID;
                aluno.Nome = entity.Nome;
                aluno.RA = entity.RA;
                aluno.Email = entity.Email;
                aluno.DataNascimento = entity.DataNascimento;
                aluno.Curso = repository.Cursos.ObterPor(entity.CursoID);
                aluno.Validar();
                repository.Alunos.Salvar(aluno);
                repository.SaveChanges();
                ViewBag.Mensagem = "Registro Salvo";
                if (id.Equals(0))
                {
                    ModelState.Clear();
                    ViewBag.CursoId = new SelectList
                   (
                       repository.Cursos.ObterCursoAtivo(),
                       "Id",
                       "Descricao"
                   );
                    return View(new Aluno());
                }
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "<br/>");
                return View("List", repository.Alunos.ObterTodos());
            }
        }
    }
}