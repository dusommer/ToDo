using System.Collections.Generic;
using prmToolkit.NotificationPattern;
using ToDo.Domain.Arguments.ListItem;
using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces.Repositories;
using ToDo.Domain.Interfaces.Services;
using System.Linq;
using ToDo.Domain.Arguments.Item;

namespace ToDo.Domain.Services
{
    public class ServiceListItem : Notifiable, IServiceListItem
    {
        private readonly IRepositoryListItem _repositoryListItem;

        public ServiceListItem()
        {

        }

        public ServiceListItem(IRepositoryListItem repositoryListItem)
        {
            _repositoryListItem = repositoryListItem;
        }

        public IEnumerable<ListItemResponse> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                AddNotification("Email", "Email is required.");
            }

            return _repositoryListItem.Get().Where(x => x.UserEmail.Equals(email)).ToList().Select(listItem => (ListItemResponse)listItem);
        }

        public InsertListItemResponse InsertListItem(InsertListItemRequest request)
        {
            if (request == null)
            {
                AddNotification("InsertListItemRequest", "Request is required.");
            }

            ListItem listItem = new ListItem(request.Name, request.UserEmail);

            AddNotifications(listItem);

            if (IsInvalid())
            {
                return null;
            }

            listItem = _repositoryListItem.Insert(listItem);

            return (InsertListItemResponse)listItem;
        }

        public RemoveListItemResponse RemoveListItem(int id)
        {
            ListItem listItem = _repositoryListItem.GetById(id);

            if (listItem == null)
            {
                AddNotification("ListItem", "List item not found.");
                return null;
            }

            _repositoryListItem.Remove(listItem);

            return new RemoveListItemResponse();
        }
    }
}
