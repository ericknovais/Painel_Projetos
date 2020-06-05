namespace Painel_Projetos.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_Unique : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProjetosGrupos", new[] { "ProjetoID" });
            DropIndex("dbo.ProjetosGrupos", new[] { "GrupoID" });
            CreateIndex("dbo.ProjetosGrupos", "ProjetoID");
            CreateIndex("dbo.ProjetosGrupos", "GrupoID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProjetosGrupos", new[] { "GrupoID" });
            DropIndex("dbo.ProjetosGrupos", new[] { "ProjetoID" });
            CreateIndex("dbo.ProjetosGrupos", "GrupoID", unique: true);
            CreateIndex("dbo.ProjetosGrupos", "ProjetoID", unique: true);
        }
    }
}
