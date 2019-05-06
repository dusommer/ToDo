using Newtonsoft.Json;
using System.Collections.Generic;
using ToDo.Models.Arguments.Item;
using ToDo.Utils;

namespace ToDo.ServicesApi
{
    public class ServiceApiItem : IServiceApiItem
    {
        public ItemResponse GetById(int id)
        {
            return JsonConvert.DeserializeObject<ItemResponse>(
                ServiceApiUtil.ApiResponse("api/item/GetById?request=" + id, "GET"));
        }

        public List<ItemResponse> GetByLisItem(int id)
        {
            return JsonConvert.DeserializeObject<List<ItemResponse>>(
                ServiceApiUtil.ApiResponse("api/item/GetByLisItem?request=" + id, "GET"));
        }

        public InsertItemResponse Insert(InsertItemRequest request)
        {
            return JsonConvert.DeserializeObject<InsertItemResponse>(
                ServiceApiUtil.ApiResponse("api/item/Insert", "POST", request));
        }

        public void Remove(int id)
        {
            ServiceApiUtil.ApiResponse("api/item/Remove?request=" + id, "GET");
        }

        public UpdateItemResponse Update(UpdateItemRequest request)
        {
            return JsonConvert.DeserializeObject<UpdateItemResponse>(
                ServiceApiUtil.ApiResponse("api/item/Update", "POST", request));
        }
    }
}