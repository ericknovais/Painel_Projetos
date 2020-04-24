namespace Painel_Projetos.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoUnico_OrdemDasColunas : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProjetosGrupos", new[] { "ProjetoID" });
            DropIndex("dbo.ProjetosGrupos", new[] { "GrupoID" });
            CreateIndex("dbo.ProjetosGrupos", "ProjetoID", unique: true);
            CreateIndex("dbo.ProjetosGrupos", "GrupoID", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProjetosGrupos", new[] { "GrupoID" });
            DropIndex("dbo.ProjetosGrupos", new[] { "ProjetoID" });
            CreateIndex("dbo.ProjetosGrupos", "GrupoID");
            CreateIndex("dbo.ProjetosGrupos", "ProjetoID");
        }
    }
}
