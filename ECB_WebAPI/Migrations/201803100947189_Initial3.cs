namespace ECB_WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EC_BANK", "Rate", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EC_BANK", "Rate", c => c.Decimal(nullable: false, precision: 10, scale: 5));
        }
    }
}
