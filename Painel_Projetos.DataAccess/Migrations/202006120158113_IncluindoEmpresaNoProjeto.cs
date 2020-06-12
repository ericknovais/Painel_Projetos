namespace Painel_Projetos.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncluindoEmpresaNoProjeto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjetosGrupos", "EmpresaID", c => c.Int());
            CreateIndex("dbo.ProjetosGrupos", "EmpresaID");
            AddForeignKey("dbo.ProjetosGrupos", "EmpresaID", "dbo.Empresas", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjetosGrupos", "EmpresaID", "dbo.Empresas");
            DropIndex("dbo.ProjetosGrupos", new[] { "EmpresaID" });
            DropColumn("dbo.ProjetosGrupos", "EmpresaID");
        }
    }
}
