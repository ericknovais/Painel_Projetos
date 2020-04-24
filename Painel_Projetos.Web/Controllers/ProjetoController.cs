using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Painel_Projetos.Web.Controllers
{
    public class ProjetoController : Controller
    {
        #region Repository
        Repository repository = new Repository();
        #endregion

        // GET: Projetos
        public ActionResult List()
        {
            IList<ProjetosGrupos> lista = new List<ProjetosGrupos>();
            try
            {
                lista = repository.ProjetosGrupos.ObterTodos();
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
            Projeto projeto = new Projeto();
            try
            {
                projeto = id.Equals(0) ? new Projeto() : repository.Projeto.ObterPor(id);
                return View(projeto);
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "</br>");
                return View(projeto);
            }
        }

        [HttpPost]
        public ActionResult Edit(Projeto entity, int id = 0)
        {
            Representante representante = repository.Representante.ObterPorNome(User.Identity.Name);
            ProjetosGrupos prjGrupos = new ProjetosGrupos();
            Projeto projeto = new Projeto();
            try
            {
                projeto = id.Equals(0) ? new Projeto() : repository.Projeto.ObterPor(id);
                projeto.Titulo = entity.Titulo;
                projeto.Descricao = entity.Descricao;
                projeto.Validar();
                repository.Projeto.Salvar(projeto);

                if (id.Equals(0))
                {
                    prjGrupos.Representante = representante;
                    prjGrupos.Projeto = projeto;
                    prjGrupos.Validar();
                    repository.ProjetosGrupos.Salvar(prjGrupos);
                }

                repository.SaveChanges();

                ViewBag.Sucesso = "Projeto cadastrado com sucesso";

                if (id.Equals(0))
                {
                    ModelState.Clear();
                    return View(new Projeto());
                }
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "</br>");
                return View(entity);
            }
        }
    }
}