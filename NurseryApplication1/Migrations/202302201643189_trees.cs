namespace NurseryApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trees : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trees",
                c => new
                    {
                        TreeId = c.Int(nullable: false, identity: true),
                        TreeName = c.String(),
                        TreeHeight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TreeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Trees");
        }
    }
}
