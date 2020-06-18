using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                var identity = User.Identity as ClaimsIdentity;
                var login = identity.Claims.FirstOrDefault(x => x.Type == "Login").Value;
                var usuario = repository.Usuario.ObterPeloLogin(login);
                if (usuario.Perfil.Equals(Perfil.Representante))
                {
                    lista = repository.ProjetosGrupos.ObterProjetoRepresentante(Convert.ToInt32(usuario.RepresentanteID));
                    if (lista.Count.Equals(0))
                        TempData["ListaVazia"] = "Você não possui projetos cadastrados";
                }
                else
                {
                    lista = repository.ProjetosGrupos.ObterTodos();
                    if (lista.Count.Equals(0))
                        TempData["ListaVazia"] = "Não ha projetos disponíveis no momento";
                }
                return View(lista);
            }
            catch (Exception ex)
            {
                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
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
                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
                return View(projeto);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
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
                    prjGrupos.RepresentanteId = representante.ID;
                    prjGrupos.ProjetoID = projeto.ID;
                    prjGrupos.EmpresaID = repository.Empresa.ObterEmpresaPeloRepresentante(representante.ID).ID;
                    prjGrupos.Validar();
                    repository.ProjetosGrupos.Salvar(prjGrupos);
                }

                repository.SaveChanges();
                if (id.Equals(0))
                {
                    TempData["Mensagem"] = "Sucesso";
                    ModelState.Clear();
                    return View(new Projeto());
                }
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
                return View(entity);
            }
        }

        public ActionResult Details(int id = 0)
        {
            Projeto projeto = new Projeto();
            try
            {
                projeto = id.Equals(0) ? new Projeto() : repository.Projeto.ObterPor(id);
                return View(projeto);
            }
            catch (Exception ex)
            {
                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
                return View(projeto);
            }
        }

    }
}