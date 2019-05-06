using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ToDo.Api.Controllers.Base;
using ToDo.Domain.Arguments.Item;
using ToDo.Domain.Interfaces.Services;
using ToDo.Infra.Transaction;

namespace ToDo.Api.Controllers
{
    [RoutePrefix("api/item")]
    public class ItemApiController : BaseController
    {
        private readonly IServiceItem _serviceItem;
        public ItemApiController(IUnitOfWork unitOfWork, IServiceItem serviceItem) : base(unitOfWork)
        {
            _serviceItem = serviceItem;
        }

        [Route("Insert")]
        [HttpPost]
        public async Task<HttpResponseMessage> Insert(InsertItemRequest request)
        {
            return await ExecuteResponse(request, "Insert");
        }

        [Route("Update")]
        [HttpPost]
        public async Task<HttpResponseMessage> Update(UpdateItemRequest request)
        {
            return await ExecuteResponse(request, "Update");
        }

        [Route("GetById")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetById(int request)
        {
            return await ExecuteResponse(request, "GetById");
        }

        [Route("GetByLisItem")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetByLisItem(int request)
        {
            return await ExecuteResponse(request, "GetByLisItem");
        }

        [Route("Remove")]
        [HttpGet]
        public async Task<HttpResponseMessage> Remove(int request)
        {
            return await ExecuteResponse(request, "Remove");
        }

        private async Task<HttpResponseMessage> ExecuteResponse(object request, string method)
        {
            try
            {
                object response = null;

                switch (method)
                {
                    case "Insert":
                        response = _serviceItem.InsertItem(request as InsertItemRequest);
                        break;
                    case "Update":
                        response = _serviceItem.UpdateItem(request as UpdateItemRequest);
                        break;
                    case "GetById":
                        response = _serviceItem.GetById(int.Parse(request.ToString()));
                        break;
                    case "GetByLisItem":
                        response = _serviceItem.GetByListItem(int.Parse(request.ToString()));
                        break;
                    case "Remove":
                        response = _serviceItem.RemoveItem(int.Parse(request.ToString()));
                        break;
                    default:
                        break;
                }

                return await ResponseAsync(response, _serviceItem);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }
    }
}