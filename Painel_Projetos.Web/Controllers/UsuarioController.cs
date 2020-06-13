using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using Painel_Projetos.Web.ViewModels;
using System;
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

            Usuario usuario = repository.Usuario.ObterPeloLogin(entity.Login);

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
                case Perfil.Coordenador:
                    var coordenador = repository.Cordenador.ObterPor(Convert.ToInt32(usuario.CoordenadorID));
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, coordenador.Nome),
                        new Claim("Login", usuario.Login),
                        new Claim(ClaimTypes.Role, usuario.Perfil.ToString()),
                    }, "ApplicationCookie");
                    break;
                case Perfil.Aluno:
                    var aluno = repository.Aluno.ObterPor(Convert.ToInt32(usuario.AlunoID));
                    identity = new ClaimsIdentity(new[]
                     {
                        new Claim(ClaimTypes.Name, aluno.Nome),
                        new Claim("Login", usuario.Login),
                        new Claim(ClaimTypes.Role, usuario.Perfil.ToString()),
                    }, "ApplicationCookie");
                    break;
                case Perfil.Representante:
                    var representante = repository.Representante.ObterPor(Convert.ToInt32(usuario.RepresentanteID));
                    identity = new ClaimsIdentity(new[]
                     {
                        new Claim(ClaimTypes.Name, representante.Nome),
                        new Claim("Login", usuario.Login),
                        new Claim(ClaimTypes.Role, usuario.Perfil.ToString()),
                    }, "ApplicationCookie");
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

        [Authorize]
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View();

            var identity = User.Identity as ClaimsIdentity;
            var login = identity.Claims.FirstOrDefault(x => x.Type == "Login").Value;

            var usuario = repository.Usuario.ObterPeloLogin(login);

            if (Usuario.Encriptar(viewModel.SenhaAtual) != usuario.Senha)
            {
                TempData["Alerta"] = "Senha atual informada está incorreta";
                return View();
            }

            if (Usuario.Encriptar(viewModel.NovaSenha) == usuario.Senha)
            {
                TempData["Alerta"] = "Nova senha não poder ser a mesma que a senha atual";
                return View();
            }

            usuario.Senha = Usuario.Encriptar(viewModel.NovaSenha);
            try
            {
                repository.SaveChanges();
                TempData["SenhaAlterada"] = "Senha alterada com sucesso";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
                return View();
            }

        }

        public ActionResult EsqueciSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EsqueciSenha(EsqueciSenhaViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View();

            var usuairo = repository.Usuario.ObterPeloLogin(Usuario.SepararEmail(viewModel.Email));
            if (usuairo.ID > 0)
            {
                switch (usuairo.Perfil)
                {
                    case Perfil.Coordenador:
                        var coordenador = repository.Usuario.ObterCoordenador(Convert.ToInt32(usuairo.CoordenadorID));

                        if (coordenador.Coordenador.Email == viewModel.Email)
                        {
                            string novaSenha = Usuario.geraSenha();
                            usuairo.Senha = Usuario.Encriptar(novaSenha);
                            try
                            {
                                repository.Usuario.Salvar(usuairo);
                                repository.SaveChanges();
                                Usuario.EnviarEmailDeNovaSenha(coordenador.Coordenador.Nome, coordenador.Coordenador.Email, novaSenha);
                            }
                            catch (Exception ex)
                            {
                                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
                                return View(viewModel);
                            }
                        }
                        else
                        {
                            TempData["Alerta"] = "E-mail informado não localizado no cadastro";
                            return View(viewModel);
                        }
                        break;
                    case Perfil.Aluno:
                        var aluno = repository.Usuario.ObterAluno(Convert.ToInt32(usuairo.AlunoID));

                        if (aluno.Aluno.Email == viewModel.Email)
                        {
                            string novaSenha = Usuario.geraSenha();
                            usuairo.Senha = Usuario.Encriptar(novaSenha);
                            try
                            {
                                repository.Usuario.Salvar(usuairo);
                                repository.SaveChanges();
                                Usuario.EnviarEmailDeNovaSenha(aluno.Aluno.Nome, aluno.Aluno.Email, novaSenha);
                            }
                            catch (Exception ex)
                            {
                                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
                                return View(viewModel);
                            }
                        }
                        else
                        {
                            TempData["Alerta"] = "E-mail informado não localizado no cadastro";
                            return View(viewModel);
                        }
                        break;
                    case Perfil.Representante:
                        var representante = repository.Usuario.ObterRepresentante(Convert.ToInt32(usuairo.RepresentanteID));

                        if (representante.Representante.Email == viewModel.Email)
                        {
                            string novaSenha = Usuario.geraSenha();
                            usuairo.Senha = Usuario.Encriptar(novaSenha);
                            try
                            {
                                repository.Usuario.Salvar(usuairo);
                                repository.SaveChanges();
                                Usuario.EnviarEmailDeNovaSenha(representante.Representante.Nome, representante.Representante.Email, novaSenha);
                            }
                            catch (Exception ex)
                            {
                                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
                                return View(viewModel);
                            }
                        }
                        else
                        {
                            TempData["Alerta"] = "E-mail informado não localizado no cadastro";
                            return View(viewModel);
                        }
                        break;
                }
            }
            else
            {
                TempData["Alerta"] = "E-mail informado não localizado no cadastro";
                return View(viewModel);
            }
            ModelState.Clear();
            TempData["Mensagem"] = "Sucesso";
            return View();
        }
    }
}