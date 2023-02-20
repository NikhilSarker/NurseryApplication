namespace NurseryApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class caretakerstrees : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Caretakers",
                c => new
                    {
                        CaretakerId = c.Int(nullable: false, identity: true),
                        CaretakerLastName = c.String(),
                        CaretakerFirstName = c.String(),
                    })
                .PrimaryKey(t => t.CaretakerId);
            
            CreateTable(
                "dbo.CaretakerTrees",
                c => new
                    {
                        Caretaker_CaretakerId = c.Int(nullable: false),
                        Tree_TreeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Caretaker_CaretakerId, t.Tree_TreeId })
                .ForeignKey("dbo.Caretakers", t => t.Caretaker_CaretakerId, cascadeDelete: true)
                .ForeignKey("dbo.Trees", t => t.Tree_TreeId, cascadeDelete: true)
                .Index(t => t.Caretaker_CaretakerId)
                .Index(t => t.Tree_TreeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CaretakerTrees", "Tree_TreeId", "dbo.Trees");
            DropForeignKey("dbo.CaretakerTrees", "Caretaker_CaretakerId", "dbo.Caretakers");
            DropIndex("dbo.CaretakerTrees", new[] { "Tree_TreeId" });
            DropIndex("dbo.CaretakerTrees", new[] { "Caretaker_CaretakerId" });
            DropTable("dbo.CaretakerTrees");
            DropTable("dbo.Caretakers");
        }
    }
}
