namespace Painel_Projetos.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alunos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RA = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 8000, unicode: false),
                        CursoID = c.Int(nullable: false),
                        DataNascimento = c.DateTime(nullable: false),
                        Email = c.String(nullable: false, maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cursos", t => t.CursoID, cascadeDelete: true)
                .Index(t => t.CursoID);
            
            CreateTable(
                "dbo.Cursos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GruposAlunos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AlunoID = c.Int(nullable: false),
                        GrupoID = c.Int(nullable: false),
                        Administrador = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Alunos", t => t.AlunoID, cascadeDelete: true)
                .ForeignKey("dbo.Grupos", t => t.GrupoID, cascadeDelete: true)
                .Index(t => t.AlunoID)
                .Index(t => t.GrupoID);
            
            CreateTable(
                "dbo.Grupos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CNPJ = c.String(nullable: false, maxLength: 8000, unicode: false),
                        RazaoSocial = c.String(nullable: false, maxLength: 8000, unicode: false),
                        RepresentanteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Representantes", t => t.RepresentanteId, cascadeDelete: true)
                .Index(t => t.RepresentanteId);
            
            CreateTable(
                "dbo.Representantes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Email = c.String(nullable: false, maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AlunoID = c.Int(),
                        RepresentanteID = c.Int(),
                        CordenadorID = c.Int(),
                        Login = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Perfil = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Alunos", t => t.AlunoID)
                .ForeignKey("dbo.Cordenadores", t => t.CordenadorID)
                .ForeignKey("dbo.Representantes", t => t.RepresentanteID)
                .Index(t => t.AlunoID)
                .Index(t => t.RepresentanteID)
                .Index(t => t.CordenadorID);
            
            CreateTable(
                "dbo.Cordenadores",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Email = c.String(nullable: false, maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "RepresentanteID", "dbo.Representantes");
            DropForeignKey("dbo.Usuarios", "CordenadorID", "dbo.Cordenadores");
            DropForeignKey("dbo.Usuarios", "AlunoID", "dbo.Alunos");
            DropForeignKey("dbo.Empresas", "RepresentanteId", "dbo.Representantes");
            DropForeignKey("dbo.GruposAlunos", "GrupoID", "dbo.Grupos");
            DropForeignKey("dbo.GruposAlunos", "AlunoID", "dbo.Alunos");
            DropForeignKey("dbo.Alunos", "CursoID", "dbo.Cursos");
            DropIndex("dbo.Usuarios", new[] { "CordenadorID" });
            DropIndex("dbo.Usuarios", new[] { "RepresentanteID" });
            DropIndex("dbo.Usuarios", new[] { "AlunoID" });
            DropIndex("dbo.Empresas", new[] { "RepresentanteId" });
            DropIndex("dbo.GruposAlunos", new[] { "GrupoID" });
            DropIndex("dbo.GruposAlunos", new[] { "AlunoID" });
            DropIndex("dbo.Alunos", new[] { "CursoID" });
            DropTable("dbo.Cordenadores");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Representantes");
            DropTable("dbo.Empresas");
            DropTable("dbo.Grupos");
            DropTable("dbo.GruposAlunos");
            DropTable("dbo.Cursos");
            DropTable("dbo.Alunos");
        }
    }
}
