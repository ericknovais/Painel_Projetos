using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Painel_Projetos.Web.Controllers
{
    public class GrupoController : Controller
    {
        #region Repository
        Repository repository = new Repository();
        #endregion
        // GET: Grupo
        public ActionResult List()
        {
            IList<GruposAlunos> lista = new List<GruposAlunos>();
            try
            {
                lista = repository.GruposAlunos.ObterTodos();
                return View(lista);
            }
            catch (Exception ex)
            {

                return View(lista);
            }
            
        }

        public ActionResult Edit(int id = 0)
        {
            return View();
        }
    }
}