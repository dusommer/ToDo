using System.Collections.Generic;
using prmToolkit.NotificationPattern;
using ToDo.Domain.Arguments.Item;
using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces.Repositories;
using ToDo.Domain.Interfaces.Services;
using System.Linq;

namespace ToDo.Domain.Services
{
    public class ServiceItem : Notifiable, IServiceItem
    {
        private readonly IRepositoryItem _repositoryItem;

        public ServiceItem()
        {

        }

        public ServiceItem(IRepositoryItem repositoryItem)
        {
            _repositoryItem = repositoryItem;
        }

        public ItemResponse GetById(int id)
        {
            if (id == 0)
            {
                AddNotification("id", "Id item is required.");
            }
            var item = _repositoryItem.GetById(id);

            return (ItemResponse)item;
        }

        public IEnumerable<ItemResponse> GetByListItem(int idListItem)
        {
            if (idListItem == 0)
            {
                AddNotification("idListItem", "List item is required.");
            }
            var items = _repositoryItem.Get().Where(x => x.IdListItem.Equals(idListItem)).ToList().Select(item => (ItemResponse)item);
            return items;
        }

        public InsertItemResponse InsertItem(InsertItemRequest request)
        {
            if (request == null)
            {
                AddNotification("InsertItemRequest", "Request is required.");
            }

            Item item = new Item(request.Description, request.Position, request.IdListItem, request.IdParentItem);

            AddNotifications(item);

            if (IsInvalid())
            {
                return null;
            }

            item = _repositoryItem.Insert(item);

            return (InsertItemResponse)item;
        }

        public RemoveItemResponse RemoveItem(int id)
        {
            Item item = _repositoryItem.GetById(id);

            if (item == null)
            {
                AddNotification("Item", "Item not found.");
                return null;
            }

            _repositoryItem.Remove(item);

            return new RemoveItemResponse();
        }

        public UpdateItemResponse UpdateItem(UpdateItemRequest request)
        {
            if (request == null)
            {
                AddNotification("UpdateItemRequest", "Request is required.");
            }
            Item item = _repositoryItem.GetById(request.Id);

            if (item == null)
            {
                AddNotification("Item", "Item not found.");
                return null;
            }
            item.Update(request);
            AddNotifications(item);

            if (IsInvalid())
            {
                return null;
            }

            _repositoryItem.Update(item);

            return (UpdateItemResponse)item;
        }
    }
}
