using ToDo.Domain.Interfaces.Arguments;

namespace ToDo.Domain.Arguments.ListItem
{
    public class UpdateItemRequest : IRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public int IdParentItem { get; set; }
    }
}
