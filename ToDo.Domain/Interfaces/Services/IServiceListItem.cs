using System.Collections.Generic;
using ToDo.Domain.Arguments.ListItem;
using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces.Services.Base;

namespace ToDo.Domain.Interfaces.Services
{
    public interface IServiceListItem : IServiceBase
    {
        InsertListItemResponse InsertListItem(InsertListItemRequest request);
        IEnumerable<ListItem> GetByEmail(string email);
        RemoveListItemResponse RemoveListItem(int id);
    }
}
