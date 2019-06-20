namespace JewelryStore.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerPAssword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Password");
        }
    }
}
