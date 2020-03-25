using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using Painel_Projetos.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;


namespace Painel_Projetos.Web.Controllers
{
    public class UsuarioController : Controller
    {
        #region Repository
        Repository repository = new Repository();
        #endregion

        public ActionResult Login(string ReturnUrl)
        {
            LoginViewModel viewModel = new LoginViewModel
            {
                UrlRetorno = ReturnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel entity)
        {
            if (!ModelState.IsValid)
                return View(entity);

            Usuario usuario = repository.Usuario.ObterSenhaPor(entity.Login);

            if (usuario == null)
            {
                ModelState.AddModelError("Login", "Login incorreto");
                return View(entity);
            }

            if (usuario.Senha != Usuario.Encriptar(entity.Senha))
            {
                ModelState.AddModelError("Senha", "Senha incorreta");
                return View(entity);
            }

            ClaimsIdentity identity = null;

            switch (usuario.Perfil)
            {
                case Perfil.Cordenador:
                    break;
                case Perfil.Aluno:
                    var userA = repository.Aluno.ObterPor(Convert.ToInt32(usuario.AlunoID));
                    identity = new ClaimsIdentity(new[]
                     {
                        new Claim(ClaimTypes.Name, userA.Nome),
                        new Claim("Login", usuario.Login),
                        new Claim("Perfil", usuario.Perfil.ToString()),
                    }, "ApplicationCookie");
                    break;
                case Perfil.Representante:
                    var userR = repository.Representante.ObterPor(Convert.ToInt32(usuario.RepresentanteID));
                    identity = new ClaimsIdentity(new[]
                     {
                        new Claim(ClaimTypes.Name, userR.Nome),
                        new Claim("Login", usuario.Login),
                        new Claim("Perfil", usuario.Perfil.ToString()),
                    }, "ApplicationCookie");
                    break;
                default:
                    break;
            }

            Request.GetOwinContext().Authentication.SignIn(identity);
            if (!String.IsNullOrWhiteSpace(entity.UrlRetorno) || Url.IsLocalUrl(entity.UrlRetorno))
                return Redirect(entity.UrlRetorno);
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Usuario");
        }
    }
}