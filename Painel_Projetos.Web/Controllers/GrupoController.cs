using Painel_Projetos.DataAccess.GenericAbstract;
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
            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            return View();
        }
    }
}