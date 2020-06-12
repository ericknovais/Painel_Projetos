namespace Painel_Projetos.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Projeto_CampoGrupoNull : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProjetosGrupos", new[] { "GrupoID" });
            CreateIndex("dbo.ProjetosGrupos", "GrupoID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProjetosGrupos", new[] { "GrupoID" });
            CreateIndex("dbo.ProjetosGrupos", "GrupoID", unique: true);
        }
    }
}
