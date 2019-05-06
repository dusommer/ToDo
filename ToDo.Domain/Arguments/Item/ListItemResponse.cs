namespace ToDo.Domain.Arguments.Item
{
    public class ListItemResponse
    {
        public static explicit operator ListItemResponse(Entities.ListItem listItem)
        {
            if (listItem == null)
            {
                return null;
            }

            return new ListItemResponse()
            {
                Id = listItem.Id,
                Name = listItem.Name,
                UserEmail = listItem.UserEmail
            };
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserEmail { get; set; }
    }
}
