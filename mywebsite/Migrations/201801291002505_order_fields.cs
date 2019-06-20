namespace JewelryStore.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order_fields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "FirstName", c => c.String(nullable: false, maxLength: 160));
            AlterColumn("dbo.Orders", "LastName", c => c.String(maxLength: 160));
            AlterColumn("dbo.Orders", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Shipping", c => c.String(nullable: false, maxLength: 70));
            AlterColumn("dbo.Orders", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Phone", c => c.String());
            AlterColumn("dbo.Orders", "Shipping", c => c.String());
            AlterColumn("dbo.Orders", "Email", c => c.String());
            AlterColumn("dbo.Orders", "LastName", c => c.String());
            AlterColumn("dbo.Orders", "FirstName", c => c.String());
        }
    }
}
