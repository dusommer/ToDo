namespace ToDo.Models.Arguments.Item
{
    public class ItemResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public int IdListItem { get; set; }
        public int? IdParentItem { get; set; }
    }
}
