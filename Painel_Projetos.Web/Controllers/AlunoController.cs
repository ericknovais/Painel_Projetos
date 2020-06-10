using AutenticacaoNoAspNetMVC.Filters;
using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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
            if (entity.CursoID == 0)
            {
                ModelState.AddModelError("CursoID", "Selecione um curso");
                ViewBag.CursoId = new SelectList
                   (
                       repository.Curso.ObterCursoAtivo(),
                       "Id",
                       "Descricao",
                       entity.CursoID
                   );
                return View(entity);
            }

            Aluno aluno = new Aluno();
            Usuario usuario = new Usuario();
            try
            {
                aluno = id.Equals(0) ? new Aluno() : repository.Aluno.ObterPor(id);
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
                    usuario.Senha = Usuario.Encriptar(Usuario.SenhaPadrao);
                    usuario.Perfil = Perfil.Aluno;
                    usuario.Validar();
                    repository.Usuario.Salvar(usuario);
                    Usuario.EnviarEmailDeLogin(aluno.Nome, aluno.Email);
                }

                repository.SaveChanges();
                
                
                if (id.Equals(0))
                {
                    TempData["Mensagem"] = "Sucesso";
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
            catch (Exception ex)
            {
                string[] mensagens = ex.Message.Replace("\r\n", "x").Split('x');

                for (int i = 0; i < mensagens.Count() - 1; i++)
                {
                    if (mensagens[i].Contains(aluno.MsgRA))
                        ModelState.AddModelError("RA", mensagens[i]);
                    else if(mensagens[i].Contains(aluno.MsgEmail))
                        ModelState.AddModelError("Email", mensagens[i]);
                    else
                        ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "<br/>");
                }

                ViewBag.CursoId = new SelectList
                  (
                      repository.Curso.ObterCursoAtivo(),
                      "Id",
                      "Descricao",
                      entity.CursoID
                  );
                return View(entity);
            }
        }
    }
}