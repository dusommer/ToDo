using Newtonsoft.Json;
using System.Collections.Generic;
using ToDo.Models.Arguments.ListItem;
using ToDo.Utils;

namespace ToDo.ServicesApi
{
    public class ServiceApiListItem : IServiceApiListItem
    {
        public List<ListItemResponse> GetByEmail(string email)
        {
            return JsonConvert.DeserializeObject<List<ListItemResponse>>(
                ServiceApiUtil.ApiResponse("api/listItem/GetByEmail?request=" + email, "GET"));
        }

        public InsertListItemResponse Insert(string name, string userEmail)
        {
            var request = new InsertListItemRequest()
            {
                Name = name,
                UserEmail = userEmail
            };

            return JsonConvert.DeserializeObject<InsertListItemResponse>(
                ServiceApiUtil.ApiResponse("api/listItem/Insert", "POST", request));
        }

        public void Remove(string id)
        {
            ServiceApiUtil.ApiResponse("api/listItem/Remove?request=" + id, "GET");
        }
    }
}