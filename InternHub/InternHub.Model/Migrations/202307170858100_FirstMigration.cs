namespace InternHub.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "StateId", "dbo.States");
            DropIndex("dbo.AspNetUsers", new[] { "StateId" });
            DropColumn("dbo.AspNetUsers", "StateId");
            DropTable("dbo.States");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        CreatedByUserId = c.String(),
                        UpdatedByUserId = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "StateId", c => c.Guid());
            CreateIndex("dbo.AspNetUsers", "StateId");
            AddForeignKey("dbo.AspNetUsers", "StateId", "dbo.States", "Id", cascadeDelete: true);
        }
    }
}
