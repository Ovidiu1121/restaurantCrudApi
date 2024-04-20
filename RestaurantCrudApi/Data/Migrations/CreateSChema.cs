using FluentMigrator;

namespace RestaurantCrudApi.Data.Migrations
{
    [Migration(22032024)]
    public class CreateSChema: Migration
    {
        public override void Up()
        {
            Create.Table("movie")
                   .WithColumn("id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("title").AsString(128).NotNullable()
                     .WithColumn("duration").AsString(128).NotNullable()
                      .WithColumn("rating").AsInt32().NotNullable();
        }
        public override void Down()
        {

        }

    }
}
