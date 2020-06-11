namespace Painel_Projetos.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoNomedaTabela : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Cordenadores", newName: "Coordenadores");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Coordenadores", newName: "Cordenadores");
        }
    }
}
