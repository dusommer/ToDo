using prmToolkit.NotificationPattern;
using ToDo.Domain.Arguments.Item;
using ToDo.Domain.Entities.Base;

namespace ToDo.Domain.Entities
{
    public class Item : EntityBase
    {
        protected Item()
        {

        }

        public Item(string description, int position, int idListItem, int? idParentItem = null)
        {
            Description = description;
            Position = position;
            IdListItem = idListItem;
            if (idParentItem != null)
            {
                IdParentItem = idParentItem;
            }
            Validate();
        }

        public string Description { get; private set; }
        public int Position { get; private set; }
        public int IdListItem { get; private set; }
        public int? IdParentItem { get; private set; }

        public virtual ListItem ListItem { get; set; }
        public virtual Item ParentItem { get; set; }

        public override void Validate()
        {
            new AddNotifications<Item>(this)
                .IfNullOrEmpty(x => x.Description, "Description is required.")
                .IfAreEquals(x => x.IdListItem, 0, "List item is required.");
        }

        public void Update(UpdateItemRequest request)
        {
            Description = request.Description;
            Position = request.Position;
            IdParentItem = request.IdParentItem;
            Validate();
        }
    }
}
