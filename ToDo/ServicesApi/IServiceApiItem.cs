using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDo.Models.Arguments.Item;

namespace ToDo.ServicesApi
{
    public interface IServiceApiItem
    {
        InsertItemResponse Insert(InsertItemRequest request);
        UpdateItemResponse Update(UpdateItemRequest request);
        ItemResponse GetById(int id);
        List<ItemResponse> GetByLisItem(int id);
        void Remove(int id);
    }
}