using ToDo.Domain.Interfaces.Arguments;

namespace ToDo.Domain.Arguments.ListItem
{
    public class UpdateItemResponse : IResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public int IdParentItem { get; set; }

        public string Message { get; set; }

        public static explicit operator UpdateItemResponse(Entities.Item item)
        {
            return new UpdateItemResponse()
            {
                Id = item.Id,
                Description = item.Description,
                Position = item.Position,
                IdParentItem = item.IdParentItem,
                Message = "Item updated successfully."
            };
        }
    }
}
