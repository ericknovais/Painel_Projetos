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
    public class CoordenadorController : Controller
    {
        #region Repository
        Repository repository = new Repository();
        #endregion

        [AutorizacaoTipo(new[] { Perfil.Cordenador })]
        // GET: Cordenador
        public ActionResult List()
        {
            IList<Coordenador> cordenadores = new List<Coordenador>();
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
            Coordenador cordenador = new Coordenador();
            try
            {
                cordenador = id.Equals(0) ? new Coordenador() : repository.Cordenador.ObterPor(id);
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
        public ActionResult Edit(Coordenador entity, int id = 0)
        {
            Coordenador cordenador = new Coordenador();
            Usuario usuario = new Usuario();
            try
            {
                cordenador = id.Equals(0) ? new Coordenador() : repository.Cordenador.ObterPor(id);
                cordenador.Nome = entity.Nome;
                cordenador.Email = entity.Email;
                cordenador.Validar();
                repository.Cordenador.Salvar(cordenador);

                if (id.Equals(0))
                {
                    usuario.Coordenador = cordenador;
                    usuario.Login = Usuario.SepararEmail(cordenador.Email);
                    var senha = Usuario.geraSenha();
                    usuario.Senha = Usuario.Encriptar(senha);
                    usuario.Perfil = Perfil.Cordenador;
                    usuario.Validar();
                    repository.Usuario.Salvar(usuario);
                    Usuario.EnviarEmailDeLogin(cordenador.Nome, cordenador.Email, senha);
                }

                repository.SaveChanges();

                
                if (id.Equals(0))
                {
                    TempData["Mensagem"] = "Sucesso";

                    ModelState.Clear();
                    return View(new Coordenador());
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
                        TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "<br/>");
                }
                return View(entity);
            }
        }
    }
}