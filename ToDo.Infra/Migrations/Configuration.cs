namespace ToDo.Infra.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ToDo.Domain.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<ToDo.Infra.Persistence.ToDoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ToDo.Infra.Persistence.ToDoContext context)
        {
            context.ListItems.AddOrUpdate(x => x.Id,
                new ListItem("Sprint 02/2019 A", "dusommer@gmail.com")
            );
            context.Items.AddOrUpdate(x => x.Id,
                new Item("Step 1", 0, 1, 1),
                new Item("Do", 1, 1, 1),
                new Item("Do", 2, 1, 1)
            );
        }
    }
}
