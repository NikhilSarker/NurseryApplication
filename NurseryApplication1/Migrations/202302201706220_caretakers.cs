namespace NurseryApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class caretakers : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CaretakerTrees", newName: "TreeCaretakers");
            DropPrimaryKey("dbo.TreeCaretakers");
            AddPrimaryKey("dbo.TreeCaretakers", new[] { "Tree_TreeId", "Caretaker_CaretakerId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TreeCaretakers");
            AddPrimaryKey("dbo.TreeCaretakers", new[] { "Caretaker_CaretakerId", "Tree_TreeId" });
            RenameTable(name: "dbo.TreeCaretakers", newName: "CaretakerTrees");
        }
    }
}
