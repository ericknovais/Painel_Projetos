using Painel_Projetos.DomainModel.Class;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("VARCHAR"));
            modelBuilder.Properties().Configure(p => p.IsRequired());
        }

    }
}
