namespace Painel_Projetos.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColunaPeriodoEmAluno : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alunos", "MyProperty", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Alunos", "MyProperty");
        }
    }
}
