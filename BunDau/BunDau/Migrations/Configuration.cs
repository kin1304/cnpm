namespace BunDau.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BunDau.DataBase.DbContextLocal>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BunDau.DataBase.DbContextLocal";
        }

        protected override void Seed(BunDau.DataBase.DbContextLocal context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
