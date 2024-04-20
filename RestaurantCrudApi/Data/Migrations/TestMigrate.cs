using FluentMigrator;

namespace RestaurantCrudApi.Data.Migrations
{
    [Migration(23320241144)]
    public class TestMigrate: Migration
    {

        public override void Up()
        {
            Execute.Script(@"./Data/Scripts/data.sql");
        }
        public override void Down()
        {

        }

    }
}
