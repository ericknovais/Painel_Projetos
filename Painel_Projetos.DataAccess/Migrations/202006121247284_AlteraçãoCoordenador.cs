namespace Painel_Projetos.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteraçãoCoordenador : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Usuarios", name: "CordenadorID", newName: "CoordenadorID");
            RenameIndex(table: "dbo.Usuarios", name: "IX_CordenadorID", newName: "IX_CoordenadorID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Usuarios", name: "IX_CoordenadorID", newName: "IX_CordenadorID");
            RenameColumn(table: "dbo.Usuarios", name: "CoordenadorID", newName: "CordenadorID");
        }
    }
}
