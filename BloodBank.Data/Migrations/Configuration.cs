namespace BloodBank.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BloodBank.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BloodBank.Data.ApplicationDbContext Context)
        {
            var TestMyClass = new BloodTable() { Type = BloodType.Ap };
            Context.Inventory.Add(TestMyClass);

            // Normal seeding goes here
            Context.Inventory.Add(new BloodTable() { Type = BloodType.An });
            Context.Inventory.Add(new BloodTable() { Type = BloodType.Bp });
            Context.Inventory.Add(new BloodTable() { Type = BloodType.Bn });
            Context.Inventory.Add(new BloodTable() { Type = BloodType.ABp });
            Context.Inventory.Add(new BloodTable() { Type = BloodType.ABn });
            Context.Inventory.Add(new BloodTable() { Type = BloodType.Op });
            Context.Inventory.Add(new BloodTable() { Type = BloodType.On });
        }
    }
}
