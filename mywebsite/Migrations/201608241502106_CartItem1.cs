namespace JewelryStore.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CartItem1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CartItems", name: "Order_OrderId", newName: "OrderId");
            RenameIndex(table: "dbo.CartItems", name: "IX_Order_OrderId", newName: "IX_OrderId");
            AddColumn("dbo.CartItems", "SessionId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CartItems", "SessionId");
            RenameIndex(table: "dbo.CartItems", name: "IX_OrderId", newName: "IX_Order_OrderId");
            RenameColumn(table: "dbo.CartItems", name: "OrderId", newName: "Order_OrderId");
        }
    }
}
