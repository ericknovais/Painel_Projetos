using AutenticacaoNoAspNetMVC.Filters;
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

        [AutorizacaoTipo(new[] { Perfil.Cordenador })]
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

        [AutorizacaoTipo(new[] { Perfil.Cordenador })]
        public ActionResult Edit(int id = 0)
        {
            Aluno entity = new Aluno();
            try
            {
                entity = id.Equals(0) ? new Aluno() : repository.Aluno.ObterPor(id);
                #region ViewBag.CursoId
                ViewBag.CursoId = new SelectList
                    (
                        repository.Curso.ObterCursoAtivo(),
                        "Id",
                        "Descricao",
                        entity.CursoID
                    );
                #endregion

                return View(entity);
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "</br>");
                return View(entity);
            }
        }

        [AutorizacaoTipo(new[] { Perfil.Cordenador })]
        [HttpPost]
        public ActionResult Edit(Aluno entity, int id = 0)
        {
            Aluno aluno = new Aluno();
            Usuario usuario = new Usuario();
            try
            {
                aluno = id.Equals(0) ? new Aluno() : repository.Aluno.ObterPor(id);
                //aluno.ID = entity.ID;
                aluno.Nome = entity.Nome;
                aluno.RA = entity.RA;
                aluno.Email = entity.Email;
                aluno.DataNascimento = entity.DataNascimento.Date;
                aluno.Curso = repository.Curso.ObterPor(entity.CursoID);
                aluno.Validar();
                repository.Aluno.Salvar(aluno);

                if (id.Equals(0))
                {
                    usuario.Aluno = aluno;
                    usuario.Login = Usuario.SepararEmail(aluno.Email);
                    usuario.Senha = Usuario.Encriptar("impacta2020");
                    usuario.Perfil = Perfil.Aluno;
                    usuario.Validar();
                    repository.Usuario.Salvar(usuario);
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