namespace BunDau.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasicSettings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeOfBasicSettings = c.Int(nullable: false),
                        BoolValue = c.Boolean(nullable: false),
                        IntValue = c.Int(nullable: false),
                        Value = c.String(maxLength: 255),
                        Deleted = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Default = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EntityProfile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Entity = c.String(),
                        OriginalType = c.String(),
                        Deleted = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Default = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EntityProfileItem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Group = c.String(),
                        Visible = c.Boolean(nullable: false),
                        VisibleInTheGrid = c.Boolean(nullable: false),
                        Required = c.Boolean(nullable: false),
                        Advanced = c.Boolean(nullable: false),
                        Searchable = c.Boolean(nullable: false),
                        Deleted = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Default = c.Boolean(nullable: false),
                        EntityProfile_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EntityProfile", t => t.EntityProfile_ID)
                .Index(t => t.EntityProfile_ID);
            
            CreateTable(
                "dbo.Forum",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        UpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Deleted = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Default = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EntityProfileItem", "EntityProfile_ID", "dbo.EntityProfile");
            DropIndex("dbo.EntityProfileItem", new[] { "EntityProfile_ID" });
            DropTable("dbo.Forum");
            DropTable("dbo.EntityProfileItem");
            DropTable("dbo.EntityProfile");
            DropTable("dbo.BasicSettings");
        }
    }
}
