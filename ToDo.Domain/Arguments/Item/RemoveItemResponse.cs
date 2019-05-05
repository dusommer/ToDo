using ToDo.Domain.Interfaces.Arguments;

namespace ToDo.Domain.Arguments.Item
{
    public class RemoveItemResponse : IResponse
    {
        public string Message { get; set; }

        public static explicit operator RemoveItemResponse(Entities.Item listItem)
        {
            return new RemoveItemResponse()
            {
                Message = "Item removed successfully."
            };
        }
    }
}
