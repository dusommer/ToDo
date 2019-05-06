using System.Collections.Generic;
using ToDo.Models.Arguments.ListItem;

namespace ToDo.ServicesApi
{
    public interface IServiceApiListItem
    {
        List<ListItemResponse> GetByEmail(string email);
        InsertListItemResponse Insert(string name, string userEmail);
        void Remove(string id);
    }
}