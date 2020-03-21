using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Painel_Projetos.Web.Controllers
{
    public class UsuarioController : Controller
    {
        #region Repository
        Repository repository = new Repository();
        #endregion

        // GET: Usuario
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario entity)
        {
            Usuario usuario = new Usuario();
            try
            {
                if (string.IsNullOrEmpty(entity.Login))
                    usuario.Validar();
                else
                {
                    usuario = repository.Usuario.ObterSenhaPor(entity.Login);
                    if (Usuario.Desencriptar(usuario.Senha) == entity.Senha.ToLower())
                    {
                        //return RedirectToAction($"Index/?perfil={usuario.Perfil.ToString()}", "Home");
                        return RedirectToAction("Index", "Home", new { perfil = usuario.Perfil, usuarioID = usuario.ID });
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return View();
        }
    }
}