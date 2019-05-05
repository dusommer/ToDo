using ToDo.Domain.Interfaces.Arguments;

namespace ToDo.Domain.Arguments.ListItem
{
    public class RemoveListItemResponse : IResponse
    {
        public string Message { get; set; }

        public static explicit operator RemoveListItemResponse(Entities.ListItem listItem)
        {
            return new RemoveListItemResponse()
            {
                Message = "List removed successfully."
            };
        }
    }
}
