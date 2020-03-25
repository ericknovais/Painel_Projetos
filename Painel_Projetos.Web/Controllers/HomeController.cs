using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Painel_Projetos.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Repository
        Repository repository = new Repository();
        #endregion

        [Authorize]
        public ActionResult Index()
        {
            // Usuario usuario = repository.Usuario.ObterPor(usuarioID);

            //switch (perfil)
            //{
            //    case Perfil.Cordenador:
            //        break;
            //    case Perfil.Aluno:
            //        Aluno aluno = repository.Aluno.ObterPor(Convert.ToInt32(usuario.AlunoID));
            //        ViewBag.Nome = aluno.Nome;
            //        break;
            //    case Perfil.Representante:
            //        break;
            //    default:
            //        break;
            //}
            return View();
        }
    }
}