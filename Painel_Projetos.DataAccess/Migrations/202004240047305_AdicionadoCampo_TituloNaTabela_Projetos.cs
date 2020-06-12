namespace Painel_Projetos.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoCampo_TituloNaTabela_Projetos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projetos", "Titulo", c => c.String(nullable: false, maxLength: 8000, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projetos", "Titulo");
        }
    }
}
