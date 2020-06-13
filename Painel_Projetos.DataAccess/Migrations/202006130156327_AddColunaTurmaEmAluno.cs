namespace Painel_Projetos.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColunaTurmaEmAluno : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alunos", "TurmaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Alunos", "TurmaId");
            AddForeignKey("dbo.Alunos", "TurmaId", "dbo.Turmas", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Alunos", "TurmaId", "dbo.Turmas");
            DropIndex("dbo.Alunos", new[] { "TurmaId" });
            DropColumn("dbo.Alunos", "TurmaId");
        }
    }
}
