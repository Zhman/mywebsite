namespace JewelryStore.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Delivery_CustomerId", c => c.Int());
            AddColumn("dbo.Orders", "FirstName_CustomerId", c => c.Int());
            AddColumn("dbo.Orders", "LastName_CustomerId", c => c.Int());
            AddColumn("dbo.Customers", "Discount", c => c.Int());
            CreateIndex("dbo.Orders", "Delivery_CustomerId");
            CreateIndex("dbo.Orders", "FirstName_CustomerId");
            CreateIndex("dbo.Orders", "LastName_CustomerId");
            AddForeignKey("dbo.Orders", "Delivery_CustomerId", "dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Orders", "FirstName_CustomerId", "dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Orders", "LastName_CustomerId", "dbo.Customers", "CustomerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "LastName_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "FirstName_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "Delivery_CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "LastName_CustomerId" });
            DropIndex("dbo.Orders", new[] { "FirstName_CustomerId" });
            DropIndex("dbo.Orders", new[] { "Delivery_CustomerId" });
            DropColumn("dbo.Customers", "Discount");
            DropColumn("dbo.Orders", "LastName_CustomerId");
            DropColumn("dbo.Orders", "FirstName_CustomerId");
            DropColumn("dbo.Orders", "Delivery_CustomerId");
        }
    }
}
