namespace ToDo.Domain.Arguments.Item
{
    public class ItemResponse
    {
        public static explicit operator ItemResponse(Entities.Item item)
        {
            if (item == null)
            {
                return null;
            }

            return new ItemResponse()
            {
                Id = item.Id,
                Description = item.Description,
                Position = item.Position,
                IdListItem = item.IdListItem,
                IdParentItem = item.IdParentItem
            };
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public int IdListItem { get; set; }
        public int? IdParentItem { get; set; }
    }
}
