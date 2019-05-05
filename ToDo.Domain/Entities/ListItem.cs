using prmToolkit.NotificationPattern;
using System.Collections.Generic;
using ToDo.Domain.Entities.Base;

namespace ToDo.Domain.Entities
{
    public class ListItem : EntityBase
    {
        public ListItem(string name, string userEmail)
        {
            Name = name;
            Items = new HashSet<Item>();
            UserEmail = userEmail;
        }

        protected ListItem()
        {
            Items = new HashSet<Item>();
            Validate();
        }

        public string Name { get; private set; }
        public string UserEmail { get; private set; }

        public virtual ICollection<Item> Items { get; set; }

        public override void Validate()
        {
            new AddNotifications<ListItem>(this)
                .IfNullOrEmpty(x => x.Name, "Name is required.")
                .IfNullOrEmpty(x => x.UserEmail, "User email is required.");
        }
    }
}
