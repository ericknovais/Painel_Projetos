using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DataAccess.contextDB
{
    public class dbContext : DbContext
    {
        public dbContext() : base("PpBeta")
        {
            //if (base.Database.Exists())
            //{
            //    base.Database.Delete();
            //}
            //else { base.Database.Create(); }
        }

        public DbSet<Curso> Cursos { get; set; }

        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<Representante> Representantes { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Grupo> Grupos { get; set; }

        public DbSet<GruposAlunos> GruposAlunos { get; set; }

        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<ProjetosGrupos> ProjetosGrupos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("VARCHAR"));
            modelBuilder.Entity<Usuario>().Property(prop => prop.RepresentanteID).IsOptional();
            modelBuilder.Entity<Usuario>().Property(prop => prop.AlunoID).IsOptional();
            modelBuilder.Entity<Usuario>().Property(prop => prop.CordenadorID).IsOptional();
            modelBuilder.Entity<ProjetosGrupos>().Property(prop => prop.GrupoID).IsOptional();
            modelBuilder.Properties().Configure(p => p.IsRequired());
        }

        public override int SaveChanges()
        {
            StringBuilder _msg = new StringBuilder();
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors) // <-- Coloque um Breakpoint aqui para conferir os erros de validação.
                {
                    Console.WriteLine("Entidade do tipo \"{0}\" no estado \"{1}\" tem os seguintes erros de validação:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Erro: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (DbUpdateException e)
            {
                foreach (var eve in e.Entries)
                {
                    _msg.AppendFormat("Entidade do tipo \"{0}\" no estado \"{1}\" tem os seguintes erros de validação:",
                        eve.Entity.GetType().Name, eve.State);
                }
                throw new Exception(_msg.ToString());
            }
            catch (SqlException s)
            {
                Console.WriteLine("- Message: \"{0}\", Data: \"{1}\"",
                            s.Message, s.Data);
                throw;
            }
        }
    }
}
