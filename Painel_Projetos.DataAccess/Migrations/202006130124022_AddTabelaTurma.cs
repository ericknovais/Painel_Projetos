namespace Painel_Projetos.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTabelaTurma : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Turmas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Turmas");
        }
    }
}
