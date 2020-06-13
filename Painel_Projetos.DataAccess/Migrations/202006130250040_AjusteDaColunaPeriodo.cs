namespace Painel_Projetos.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjusteDaColunaPeriodo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alunos", "Periodo", c => c.Int(nullable: false));
            DropColumn("dbo.Alunos", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Alunos", "MyProperty", c => c.Int(nullable: false));
            DropColumn("dbo.Alunos", "Periodo");
        }
    }
}
