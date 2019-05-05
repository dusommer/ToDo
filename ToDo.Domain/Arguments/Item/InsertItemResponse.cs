using ToDo.Domain.Interfaces.Arguments;

namespace ToDo.Domain.Arguments.Item
{
    public class InsertItemResponse : IResponse
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public static explicit operator InsertItemResponse(Entities.Item item)
        {
            return new InsertItemResponse()
            {
                Id = item.Id,
                Message = "Item inserted successfully."
            };
        }
    }
}
