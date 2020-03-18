using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
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
                lista = repository.Aluno.ObterTodos();
                ViewBag.CursoID = new SelectList(
                    repository.Curso.ObterTodos(),
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
                entity = id.Equals(0) ? new Aluno() : repository.Aluno.ObterPor(id);

                ViewBag.CursoId = new SelectList
                    (
                        repository.Curso.ObterCursoAtivo(),
                        "Id",
                        "Descricao",
                        entity.CursoID
                    );

                var login = id.Equals(0) ? new Login(): repository.Login.ObterAluno(entity.ID);
                var senha = Login.Desencriptar(login.Senha);
                ViewBag.Mensagem = senha;
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
            Login login = new Login();
            try
            {
                aluno = id.Equals(0) ? new Aluno() : repository.Aluno.ObterPor(id);
                aluno.ID = entity.ID;
                aluno.Nome = entity.Nome;
                aluno.RA = entity.RA;
                aluno.Email = entity.Email;
                aluno.DataNascimento = entity.DataNascimento;
                aluno.Curso = repository.Curso.ObterPor(entity.CursoID);
                aluno.Validar();
                repository.Aluno.Salvar(aluno);

                if (id.Equals(0))
                {
                    login.AlunoId = aluno.ID;
                    login.Usuario = Login.SepararEmail(aluno.Email); 
                    login.Senha = Login.Encriptar("impacta2020");
                    login.Perfil = Perfil.Aluno;
                    repository.Login.Salvar(login);
                }
                
                repository.SaveChanges();

                ViewBag.Mensagem = "Registro Salvo";
                if (id.Equals(0))
                {
                    ModelState.Clear();
                    ViewBag.CursoId = new SelectList
                   (
                       repository.Curso.ObterCursoAtivo(),
                       "Id",
                       "Descricao"
                   );
                    return View(new Aluno());
                }
                return RedirectToAction("List");
            }
            catch (EntityException ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "<br/>");
                return View("List", repository.Aluno.ObterTodos());
            }
        }
    }
}