namespace ToDo.Models.Arguments.Item
{
    public class InsertItemRequest 
    {
        public string Description { get; set; }
        public int Position { get; set; }
        public int IdListItem { get; set; }
        public int? IdParentItem { get; set; }
    }
}
