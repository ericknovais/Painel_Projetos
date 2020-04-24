namespace Painel_Projetos.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Projetos_campoNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GruposAlunos", "GrupoID", "dbo.Grupos");
            DropIndex("dbo.GruposAlunos", new[] { "GrupoID" });
            AlterColumn("dbo.GruposAlunos", "GrupoID", c => c.Int());
            CreateIndex("dbo.GruposAlunos", "GrupoID");
            AddForeignKey("dbo.GruposAlunos", "GrupoID", "dbo.Grupos", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GruposAlunos", "GrupoID", "dbo.Grupos");
            DropIndex("dbo.GruposAlunos", new[] { "GrupoID" });
            AlterColumn("dbo.GruposAlunos", "GrupoID", c => c.Int(nullable: false));
            CreateIndex("dbo.GruposAlunos", "GrupoID");
            AddForeignKey("dbo.GruposAlunos", "GrupoID", "dbo.Grupos", "ID", cascadeDelete: true);
        }
    }
}
