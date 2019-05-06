namespace ToDo.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 255, unicode: false),
                        Position = c.Int(nullable: false),
                        IdListItem = c.Int(nullable: false),
                        IdParentItem = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ListItem", t => t.IdListItem, cascadeDelete: true)
                .ForeignKey("dbo.Item", t => t.IdParentItem)
                .Index(t => t.IdListItem)
                .Index(t => t.IdParentItem);
            
            CreateTable(
                "dbo.ListItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                        UserEmail = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Item", "IdParentItem", "dbo.Item");
            DropForeignKey("dbo.Item", "IdListItem", "dbo.ListItem");
            DropIndex("dbo.Item", new[] { "IdParentItem" });
            DropIndex("dbo.Item", new[] { "IdListItem" });
            DropTable("dbo.ListItem");
            DropTable("dbo.Item");
        }
    }
}
