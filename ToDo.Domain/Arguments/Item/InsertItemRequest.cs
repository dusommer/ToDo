using ToDo.Domain.Interfaces.Arguments;

namespace ToDo.Domain.Arguments.Item
{
    public class InsertItemRequest : IRequest
    {
        public string Description { get; set; }
        public int Position { get; set; }
        public int IdListItem { get; set; }
        public int IdParentItem { get; set; }
    }
}
