using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ToDo.Domain.Entities;

namespace ToDo.Infra.Persistence
{
    public class ToDoContext : DbContext
    {
        public ToDoContext() : base("TODOConnectionString")
        {

        }

        public IDbSet<Item> Items { get; set; }
        public IDbSet<ListItem> ListItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Entity<Item>()
                .Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Item>()
                .HasOptional(c => c.ParentItem)
                .WithMany()
                .HasForeignKey(c => c.IdParentItem);

            modelBuilder.Entity<ListItem>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<ListItem>()
                .Property(e => e.UserEmail)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<ListItem>()
                .HasMany(e => e.Items)
                .WithRequired(e => e.ListItem)
                .HasForeignKey(e => e.IdListItem);




            base.OnModelCreating(modelBuilder);
        }

    }
}
