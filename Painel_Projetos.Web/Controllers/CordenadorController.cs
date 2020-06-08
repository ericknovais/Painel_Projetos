using AutenticacaoNoAspNetMVC.Filters;
using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Painel_Projetos.Web.Controllers
{
    public class CordenadorController : Controller
    {
        #region Repository
        Repository repository = new Repository();
        #endregion

        [AutorizacaoTipo(new[] { Perfil.Cordenador })]
        // GET: Cordenador
        public ActionResult List()
        {
            IList<Cordenador> cordenadores = new List<Cordenador>();
            try
            {
                cordenadores = repository.Cordenador.ObterTodos();
                return View(cordenadores);
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "</br>");
                return View(cordenadores);

            }
        }

        [AutorizacaoTipo(new[] { Perfil.Cordenador })]
        public ActionResult Edit(int id = 0)
        {
            Cordenador cordenador = new Cordenador();
            try
            {
                cordenador = id.Equals(0) ? new Cordenador() : repository.Cordenador.ObterPor(id);
                return View(cordenador);
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "</br>");
                return View(cordenador);
            }
        }

        [AutorizacaoTipo(new[] { Perfil.Cordenador })]
        [HttpPost]
        public ActionResult Edit(Cordenador entity, int id = 0)
        {
            Cordenador cordenador = new Cordenador();
            Usuario usuario = new Usuario();
            try
            {
                cordenador = id.Equals(0) ? new Cordenador() : repository.Cordenador.ObterPor(id);
                cordenador.Nome = entity.Nome;
                cordenador.Email = entity.Email;
                cordenador.Validar();
                repository.Cordenador.Salvar(cordenador);

                if (id.Equals(0))
                {
                    usuario.Cordenador = cordenador;
                    usuario.Login = Usuario.SepararEmail(cordenador.Email);
                    usuario.Senha = Usuario.Encriptar(Usuario.SenhaPadrao);
                    usuario.Perfil = Perfil.Cordenador;
                    usuario.Validar();
                    repository.Usuario.Salvar(usuario);
                    Usuario.EnviarEmailDeLogin(cordenador.Nome, cordenador.Email);
                }

                repository.SaveChanges();

                ViewBag.Sucesso = "Registro salvo";
                if (id.Equals(0))
                {
                    ModelState.Clear();
                    return View(new Cordenador());
                }
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {

                string[] mensagens = ex.Message.Replace("\r\n", "x").Split('x');

                for (int i = 0; i < mensagens.Count() - 1; i++)
                {
                    if (mensagens[i].Contains(cordenador.MsgEmail))
                        ModelState.AddModelError("Email", mensagens[i]);
                    else
                        ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "<br/>");
                }
                return View(entity);
            }
        }
    }
}