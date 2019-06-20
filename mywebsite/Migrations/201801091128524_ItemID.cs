namespace JewelryStore.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartItems", "ItemId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CartItems", "ItemId");
        }
    }
}
