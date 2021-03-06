namespace Hypnofrog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReturnContent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        ContentId = c.Int(nullable: false, identity: true),
                        HtmlContent = c.String(),
                        PageId = c.Int(),
                    })
                .PrimaryKey(t => t.ContentId)
                .ForeignKey("dbo.Pages", t => t.PageId)
                .Index(t => t.PageId);
            
            DropColumn("dbo.Pages", "FirstContent");
            DropColumn("dbo.Pages", "SecondContent");
            DropColumn("dbo.Pages", "ThirdContent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pages", "ThirdContent", c => c.String());
            AddColumn("dbo.Pages", "SecondContent", c => c.String());
            AddColumn("dbo.Pages", "FirstContent", c => c.String());
            DropForeignKey("dbo.Contents", "PageId", "dbo.Pages");
            DropIndex("dbo.Contents", new[] { "PageId" });
            DropTable("dbo.Contents");
        }
    }
}
