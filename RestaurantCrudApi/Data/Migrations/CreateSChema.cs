using FluentMigrator;

namespace RestaurantCrudApi.Data.Migrations
{
    [Migration(22032024)]
    public class CreateSChema: Migration
    {
        public override void Up()
        {
            Create.Table("restaurant")
                   .WithColumn("id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("name").AsString(128).NotNullable()
                     .WithColumn("location").AsString(128).NotNullable()
                      .WithColumn("rating").AsInt32().NotNullable();
        }
        public override void Down()
        {

        }

    }
}
