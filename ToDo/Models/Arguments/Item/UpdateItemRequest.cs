using System;

namespace ToDo.Models.Arguments.Item
{
    public class UpdateItemRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public int? IdParentItem { get; set; }

        public static explicit operator UpdateItemRequest(ItemResponse item)
        {
            return new UpdateItemRequest()
            {
                Id = item.Id,
                Description = item.Description,
                Position = item.Position,
                IdParentItem = item.IdParentItem
            };
        }
    }
}
