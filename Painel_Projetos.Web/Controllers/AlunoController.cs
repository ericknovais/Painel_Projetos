using AutenticacaoNoAspNetMVC.Filters;
using Painel_Projetos.DataAccess.GenericAbstract;
using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Painel_Projetos.Web.Controllers
{
    public class AlunoController : Controller
    {
        #region Repository
        Repository repository = new Repository();
        #endregion

        public string CursoTela { get; set; }
        public string TurmaTela { get; set; }

        public string Teste { get; set; }

        [AutorizacaoTipo(new[] { Perfil.Coordenador, Perfil.Aluno })]
        // GET: Aluno
        public ActionResult List()
        {
            #region ViewBag.Curso
            ViewBag.CursoId = new SelectList
                (
                    repository.Curso.ObterCursoAtivo(),
                    "Id",
                    "Descricao"
                );
            #endregion

            #region ViewBag.Turma
            ViewBag.TurmaId = new SelectList
                (
                    repository.Turma.ObterTodos(),
                    "Id",
                    "Descricao"

                );
            #endregion

            var identity = User.Identity as ClaimsIdentity;
            var login = identity.Claims.FirstOrDefault(x => x.Type == "Login").Value;
            var usuario = repository.Usuario.ObterPeloLogin(login);
            var aluno = repository.Usuario.ObterAluno(Convert.ToInt32(usuario.AlunoID));
            IList<Aluno> lista = new List<Aluno>();
            if (usuario.Perfil == Perfil.Aluno)
            {
                var eAdmin = repository.GruposAlunos.ObterAlunoPor(Convert.ToInt32(aluno.AlunoID));
                //If para saber se o aluno que esta logado é admim de um grupo para poder convidar outros alunos para o grupo dele                
                if(eAdmin != null)
                    if (eAdmin.Administrador == true || eAdmin != null)
                        TempData["Admin"] = "sim";

                try
                {
                    lista = repository.Aluno.ObterAlunosTurma(aluno.Aluno.CursoID, aluno.Aluno.TurmaId);
                    TempData["Turma"] = $" <b>Curso</b>: {aluno.Aluno.Curso.Descricao} <b>Turma</b>: {aluno.Aluno.Turma.Descricao}";
                    if (lista.Count.Equals(0))
                        TempData["ListaVazia"] = "Não foi encontrado alunos";
                }
                catch (Exception ex)
                {
                    TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
                }
            }
            else
            {
                TempData["ListaVazia"] = "Não foi encontrado alunos";
            }
            return View(lista);
        }

        [HttpPost]
        public ActionResult List(Aluno entity, int Alunos = 0)
        {
            IList<Aluno> lista = new List<Aluno>();
            if (Alunos != 0)
            {
                var identity = User.Identity as ClaimsIdentity;
                var login = identity.Claims.FirstOrDefault(x => x.Type == "Login").Value;
                var usuario = repository.Usuario.ObterPeloLogin(login);
                Aluno aluno = repository.Aluno.ObterPor(Convert.ToInt32(usuario.AlunoID));
                if (Alunos.Equals(1))
                {
                    lista = repository.Aluno.ObterAlunosTurma(aluno.CursoID, aluno.TurmaId);
                    TempData["Admin"] = "sim";
                }
                else
                {
                    lista = repository.Aluno.ObterAlunosQueNaoEstaoEmGrupo(aluno.CursoID, aluno.TurmaId, aluno.Periodo);
                    if (lista.Count.Equals(0))
                        TempData["ListaVazia"] = "Não foi encontrado alunos";

                    TempData["Admin"] = "sim";
                    TempData["PoderConvidar"] = "sim";
                }
                TempData["Turma"] = $" <b>Curso</b>: {repository.Curso.ObterPor(aluno.CursoID).Descricao} <b>Turma</b>: {repository.Turma.ObterPor(aluno.TurmaId).Descricao}";
            }
            else
            {

                try
                {
                    #region ViewBag.Curso
                    ViewBag.CursoId = new SelectList
                        (
                            repository.Curso.ObterCursoAtivo(),
                            "Id",
                            "Descricao",
                            entity.CursoID
                        );
                    #endregion

                    #region ViewBag.Turma
                    ViewBag.TurmaId = new SelectList
                        (
                            repository.Turma.ObterTodos(),
                            "Id",
                            "Descricao",
                            entity.TurmaId
                        );
                    #endregion

                    lista = repository.Aluno.ObterAlunosTurma(entity.CursoID, entity.TurmaId);
                    if (lista.Count.Equals(0))
                        TempData["ListaVazia"] = "Não foi encontrado alunos";
                }
                catch (Exception ex)
                {

                    TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
                }
            }
            return View(lista);
        }

        [AutorizacaoTipo(new[] { Perfil.Coordenador })]
        public ActionResult Edit(int id = 0)
        {
            Aluno entity = new Aluno();
            try
            {
                entity = id.Equals(0) ? new Aluno() : repository.Aluno.ObterPor(id);
                //entity.DataNascimento = entity.DataNascimento.Equals(DateTime.MinValue) ? DateTime.Now : entity.DataNascimento.Date;
                #region ViewBag.Curso
                ViewBag.CursoId = new SelectList
                    (
                        repository.Curso.ObterCursoAtivo(),
                        "Id",
                        "Descricao",
                        entity.CursoID
                    );
                #endregion

                #region ViewBag.Turma
                ViewBag.TurmaId = new SelectList
                    (
                        repository.Turma.ObterTodos(),
                        "Id",
                        "Descricao",
                        entity.TurmaId
                    );
                #endregion

                #region ViewBag.Periodo
                ViewBag.Periodo = from Periodo pn in Enum.GetValues(typeof(Periodo))
                                  select new SelectListItem
                                  {
                                      Value = ((int)pn).ToString(),
                                      Text = Enum.GetName(typeof(Periodo), pn),
                                      Selected = Convert.ToBoolean(entity.Periodo)
                                  };
                #endregion

                return View(entity);
            }
            catch (Exception ex)
            {
                TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "</br>");
                return View(entity);
            }
        }

        [AutorizacaoTipo(new[] { Perfil.Coordenador })]
        [HttpPost]
        public ActionResult Edit(Aluno entity, int id = 0)
        {
            if (entity.CursoID == 0 || entity.TurmaId == 0 || entity.Periodo == 0)
            {
                if (entity.CursoID == 0)
                    ModelState.AddModelError("CursoID", "Selecione um curso");
                if (entity.TurmaId == 0)
                    ModelState.AddModelError("TurmaID", "Selecione uma turma");
                if (entity.Periodo == 0)
                    ModelState.AddModelError("PeriodoID", "Selecione um periodo");

                #region Curso
                ViewBag.CursoId = new SelectList
                   (
                       repository.Curso.ObterCursoAtivo(),
                       "Id",
                       "Descricao",
                       entity.CursoID
                   );
                #endregion

                #region Turma
                ViewBag.TurmaId = new SelectList
                   (
                       repository.Turma.ObterTodos(),
                       "Id",
                       "Descricao",
                       entity.TurmaId
                   );
                #endregion

                #region Periodo
                ViewBag.Periodo = from Periodo pn in Enum.GetValues(typeof(Periodo))
                                  select new SelectListItem
                                  {
                                      Value = ((int)pn).ToString(),
                                      Text = Enum.GetName(typeof(Periodo), pn),
                                      Selected = Convert.ToBoolean(entity.Periodo)
                                  };
                #endregion

                return View(entity);
            }

            Aluno aluno = new Aluno();
            Usuario usuario = new Usuario();
            try
            {
                aluno = id.Equals(0) ? new Aluno() : repository.Aluno.ObterPor(id);
                aluno.Nome = entity.Nome;
                aluno.RA = entity.RA;
                aluno.Email = entity.Email;
                aluno.DataNascimento = entity.DataNascimento.Date;
                aluno.Curso = repository.Curso.ObterPor(entity.CursoID);
                aluno.Turma = repository.Turma.ObterPor(entity.TurmaId);
                aluno.Periodo = entity.Periodo;
                aluno.Validar();
                repository.Aluno.Salvar(aluno);
                var senha = "";

                if (id.Equals(0))
                {
                    usuario.Aluno = aluno;
                    usuario.Login = Usuario.SepararEmail(aluno.Email);
                    senha = Usuario.geraSenha();
                    usuario.Senha = Usuario.Encriptar(senha);
                    usuario.Perfil = Perfil.Aluno;
                    usuario.Validar();
                    repository.Usuario.Salvar(usuario);
                }

                repository.SaveChanges();

                if (id.Equals(0))
                {
                    Usuario.EnviarEmailDeLogin(aluno.Nome, aluno.Email, senha);
                    TempData["Mensagem"] = "Sucesso";
                    ModelState.Clear();

                    #region ViewBag.Curso
                    ViewBag.CursoId = new SelectList
                        (
                            repository.Curso.ObterCursoAtivo(),
                            "Id",
                            "Descricao"
                        );
                    #endregion

                    #region ViewBag.Turma
                    ViewBag.TurmaId = new SelectList
                        (
                            repository.Turma.ObterTodos(),
                            "Id",
                            "Descricao"
                        );
                    #endregion

                    #region ViewBag.Periodo
                    ViewBag.Periodo = from Periodo pn in Enum.GetValues(typeof(Periodo))
                                      select new SelectListItem
                                      {
                                          Value = ((int)pn).ToString(),
                                          Text = Enum.GetName(typeof(Periodo), pn)
                                      };
                    #endregion

                    return View(new Aluno());
                }
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                string[] mensagens = ex.Message.Replace("\r\n", "x").Split('x');

                for (int i = 0; i < mensagens.Count() - 1; i++)
                {
                    if (mensagens[i].Contains(aluno.MsgRA))
                        ModelState.AddModelError("RA", mensagens[i]);
                    else if (mensagens[i].Contains(aluno.MsgEmail))
                        ModelState.AddModelError("Email", mensagens[i]);
                    else
                        TempData["Alerta"] = ex.Message.Replace(Environment.NewLine, "<br/>");
                }

                #region ViewBag.Curso
                ViewBag.CursoId = new SelectList
                    (
                        repository.Curso.ObterCursoAtivo(),
                        "Id",
                        "Descricao",
                        entity.CursoID
                    );
                #endregion

                #region ViewBag.Turma
                ViewBag.TurmaId = new SelectList
                    (
                        repository.Turma.ObterTodos(),
                        "Id",
                        "Descricao",
                        entity.TurmaId
                    );
                #endregion

                #region ViewBag.Periodo
                ViewBag.Periodo = from Periodo pn in Enum.GetValues(typeof(Periodo))
                                  select new SelectListItem
                                  {
                                      Value = ((int)pn).ToString(),
                                      Text = Enum.GetName(typeof(Periodo), pn),
                                      Selected = Convert.ToBoolean(entity.Periodo)
                                  };
                #endregion

                return View(entity);
            }
        }

        public ActionResult EnviarConvite(int id = 0)
        {
            var identity = User.Identity as ClaimsIdentity;
            var login = identity.Claims.FirstOrDefault(x => x.Type == "Login").Value;
            var usuario = repository.Usuario.ObterPeloLogin(login);
            Aluno alunoAdmin = repository.Aluno.ObterPor(Convert.ToInt32(usuario.AlunoID));
            var grupoAluno = repository.GruposAlunos.ObterAlunoPor(alunoAdmin.ID);

            Aluno aluno = id.Equals(0) ? new Aluno() : repository.Aluno.ObterPor(id);

            try
            {
                Usuario.EnviarEmailDeConvite(alunoAdmin.Nome, alunoAdmin.Email, aluno.Nome, aluno.Email, repository.Grupo.ObterPor(grupoAluno.GrupoID).Nome);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("List");
        }
    }
}