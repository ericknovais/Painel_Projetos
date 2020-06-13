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

        [AutorizacaoTipo(new[] { Perfil.Coordenador })]
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

        [AutorizacaoTipo(new[] { Perfil.Coordenador })]
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
                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "<\br>");
                return View(viewModel);
            }
        }

        [AutorizacaoTipo(new[] { Perfil.Coordenador })]
        [HttpPost]
        public ActionResult Edit(EmpresaViewModel viewmodel, int id = 0)
        {

            Usuario usuario = new Usuario();

            try
            {

                Empresa empresa = id.Equals(0) ? new Empresa() : repository.Empresa.ObterPor(id);
                var teste = Empresa.IsCnpj(viewmodel.CNPJ);
                if (Empresa.IsCnpj(viewmodel.CNPJ).Equals(false))
                {
                    ModelState.AddModelError("CNPJ", "CNPJ invalido");
                    return View(viewmodel);
                }
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
                    var senha = Usuario.geraSenha();
                    usuario.Senha = Usuario.Encriptar(senha);
                    usuario.Perfil = Perfil.Representante;
                    repository.Usuario.Salvar(usuario);
                    Usuario.EnviarEmailDeLogin(representante.Nome, representante.Email, senha);
                }

                repository.SaveChanges();

                if (id.Equals(0))
                {
                    TempData["Mensagem"] = "Sucesso";
                    ModelState.Clear();
                    return View(new EmpresaViewModel());
                }
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "<\br>");
                return View(new EmpresaViewModel());
            }
        }
    }
}