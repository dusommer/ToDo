using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ToDo.Api.Controllers.Base;
using ToDo.Domain.Arguments.ListItem;
using ToDo.Domain.Interfaces.Services;
using ToDo.Infra.Transaction;

namespace ToDo.Api.Controllers
{
    [RoutePrefix("api/listItem")]
    public class ListItemApiController : BaseController
    {
        private readonly IServiceListItem _serviceListItem;
        public ListItemApiController(IUnitOfWork unitOfWork, IServiceListItem serviceListItem) : base(unitOfWork)
        {
            _serviceListItem = serviceListItem;
        }

        [Route("Insert")]
        [HttpPost]
        public async Task<HttpResponseMessage> Insert(InsertListItemRequest request)
        {
            return await ExecuteResponse(request, "Insert");
        }

        [Route("GetByEmail")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetByEmail(string request)
        {
            return await ExecuteResponse(request, "GetByEmail");
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
                        response = _serviceListItem.InsertListItem(request as InsertListItemRequest);
                        break;
                    case "GetByEmail":
                        response = _serviceListItem.GetByEmail(request.ToString());
                        break;
                    case "Remove":
                        response = _serviceListItem.RemoveListItem(int.Parse(request.ToString()));
                        break;
                    default:
                        break;
                }

                return await ResponseAsync(response, _serviceListItem);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }
    }
}