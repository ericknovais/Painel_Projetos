namespace Painel_Projetos.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Projeto_CampoGrupoNull2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GruposAlunos", "GrupoID", "dbo.Grupos");
            DropForeignKey("dbo.ProjetosGrupos", "GrupoID", "dbo.Grupos");
            DropIndex("dbo.GruposAlunos", new[] { "GrupoID" });
            DropIndex("dbo.ProjetosGrupos", new[] { "GrupoID" });
            AlterColumn("dbo.GruposAlunos", "GrupoID", c => c.Int(nullable: false));
            AlterColumn("dbo.ProjetosGrupos", "GrupoID", c => c.Int());
            CreateIndex("dbo.GruposAlunos", "GrupoID");
            CreateIndex("dbo.ProjetosGrupos", "GrupoID", unique: true);
            AddForeignKey("dbo.GruposAlunos", "GrupoID", "dbo.Grupos", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ProjetosGrupos", "GrupoID", "dbo.Grupos", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjetosGrupos", "GrupoID", "dbo.Grupos");
            DropForeignKey("dbo.GruposAlunos", "GrupoID", "dbo.Grupos");
            DropIndex("dbo.ProjetosGrupos", new[] { "GrupoID" });
            DropIndex("dbo.GruposAlunos", new[] { "GrupoID" });
            AlterColumn("dbo.ProjetosGrupos", "GrupoID", c => c.Int(nullable: false));
            AlterColumn("dbo.GruposAlunos", "GrupoID", c => c.Int());
            CreateIndex("dbo.ProjetosGrupos", "GrupoID");
            CreateIndex("dbo.GruposAlunos", "GrupoID");
            AddForeignKey("dbo.ProjetosGrupos", "GrupoID", "dbo.Grupos", "ID", cascadeDelete: true);
            AddForeignKey("dbo.GruposAlunos", "GrupoID", "dbo.Grupos", "ID");
        }
    }
}
