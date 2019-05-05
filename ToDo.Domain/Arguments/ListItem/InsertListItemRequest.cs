using ToDo.Domain.Interfaces.Arguments;

namespace ToDo.Domain.Arguments.ListItem
{
    public class InsertListItemRequest : IRequest
    {
        public string Name { get; set; }
        public string UserEmail { get; set; }
    }
}
