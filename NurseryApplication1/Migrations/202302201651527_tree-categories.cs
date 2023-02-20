namespace NurseryApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class treecategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trees", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Trees", "CategoryId");
            AddForeignKey("dbo.Trees", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trees", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Trees", new[] { "CategoryId" });
            DropColumn("dbo.Trees", "CategoryId");
        }
    }
}
