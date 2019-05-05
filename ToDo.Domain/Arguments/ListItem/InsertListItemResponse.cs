using ToDo.Domain.Interfaces.Arguments;

namespace ToDo.Domain.Arguments.ListItem
{
    public class InsertListItemResponse : IResponse
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public static explicit operator InsertListItemResponse(Entities.ListItem listItem)
        {
            return new InsertListItemResponse()
            {
                Id = listItem.Id,
                Message = "List inserted successfully."
            };
        }
    }
}
