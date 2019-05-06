using System.Collections.Generic;
using ToDo.Domain.Arguments.Item;
using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces.Services.Base;

namespace ToDo.Domain.Interfaces.Services
{
    public interface IServiceItem : IServiceBase
    {
        InsertItemResponse InsertItem(InsertItemRequest request);
        UpdateItemResponse UpdateItem(UpdateItemRequest request);
        ItemResponse GetById(int id);
        IEnumerable<ItemResponse> GetByListItem(int idListItem);
        RemoveItemResponse RemoveItem(int id);
    }
}
