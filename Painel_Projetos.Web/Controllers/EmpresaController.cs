using AutenticacaoNoAspNetMVC.Filters;
using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using Painel_Projetos.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Painel_Projetos.Web.Controllers
{
    public class EmpresaController : Controller
    {
        #region Repository
        Repository repository = new Repository();
        #endregion

        [AutorizacaoTipo(new[] { Perfil.Cordenador })]
        public ActionResult List()
        {
            IList<Empresa> empresas = new List<Empresa>();
            try
            {
                empresas = repository.Empresa.ObterTodos();
                return View(empresas);
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "<\br>");
                return View(empresas);
            }
        }

        [AutorizacaoTipo(new[] { Perfil.Cordenador })]
        public ActionResult Edit(int id = 0)
        {
            EmpresaViewModel viewModel = new EmpresaViewModel();
            try
            {
                Empresa empresa = id.Equals(0) ? new Empresa() : repository.Empresa.ObterPor(id);
                Representante representante = empresa.RepresentanteId.Equals(0) ? new Representante() : repository.Representante.ObterPor(empresa.RepresentanteId);

                viewModel.CNPJ = empresa.CNPJ;
                viewModel.RazaoSocial = empresa.RazaoSocial;
                viewModel.Nome = representante.Nome;
                viewModel.Email = representante.Email;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message.Replace(Environment.NewLine, "<\br>");
                return View(viewModel);
            }
        }

        [AutorizacaoTipo(new[] { Perfil.Cordenador })]
        [HttpPost]
        public ActionResult Edit(EmpresaViewModel viewmodel, int id = 0)
        {

            Usuario usuario = new Usuario();

            try
            {
                Empresa empresa = id.Equals(0) ? new Empresa() : repository.Empresa.ObterPor(id);
                Representante representante = empresa.RepresentanteId.Equals(0) ? new Representante() : repository.Representante.ObterPor(empresa.RepresentanteId);

                empresa.RazaoSocial = viewmodel.RazaoSocial;
                empresa.CNPJ = viewmodel.CNPJ;

                representante.Nome = viewmodel.Nome;
                representante.Email = viewmodel.Email;

                empresa.Validar();
                representante.Validar();

                repository.Representante.Salvar(representante);
                repository.Empresa.Salvar(empresa);

                if (id.Equals(0))
                {
                    usuario.Representante = representante;
                    usuario.Login = Usuario.SepararEmail(representante.Email);
                    usuario.Senha = Usuario.Encriptar("impacta2020");
                    usuario.Perfil = Perfil.Representante;
                    repository.Usuario.Salvar(usuario);
                }

                repository.SaveChanges();

                ViewBag.Sucesso = "Registro salvo com sucesso";
                if (id.Equals(0))
                {
                    ModelState.Clear();
                    return View(new EmpresaViewModel());
                }
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["msgErro"] = ex.Message.Replace(Environment.NewLine, "<\br>");
                return View(new EmpresaViewModel());
            }
        }
    }
}