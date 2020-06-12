namespace Painel_Projetos.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tabela_Projetos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projetos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProjetosGrupos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RepresentanteId = c.Int(nullable: false),
                        ProjetoID = c.Int(nullable: false),
                        GrupoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Grupos", t => t.GrupoID, cascadeDelete: true)
                .ForeignKey("dbo.Projetos", t => t.ProjetoID, cascadeDelete: true)
                .ForeignKey("dbo.Representantes", t => t.RepresentanteId, cascadeDelete: true)
                .Index(t => t.RepresentanteId)
                .Index(t => t.ProjetoID)
                .Index(t => t.GrupoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjetosGrupos", "RepresentanteId", "dbo.Representantes");
            DropForeignKey("dbo.ProjetosGrupos", "ProjetoID", "dbo.Projetos");
            DropForeignKey("dbo.ProjetosGrupos", "GrupoID", "dbo.Grupos");
            DropIndex("dbo.ProjetosGrupos", new[] { "GrupoID" });
            DropIndex("dbo.ProjetosGrupos", new[] { "ProjetoID" });
            DropIndex("dbo.ProjetosGrupos", new[] { "RepresentanteId" });
            DropTable("dbo.ProjetosGrupos");
            DropTable("dbo.Projetos");
        }
    }
}
