using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ToDo.Domain.Interfaces.Services.Base;
using ToDo.Infra.Transaction;

namespace ToDo.Api.Controllers.Base
{

    public class BaseController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private IServiceBase _serviceBase;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HttpResponseMessage> ResponseAsync(object result, IServiceBase serviceBase)
        {
            _serviceBase = serviceBase;

            if (!serviceBase.Notifications.Any())
            {
                try
                {
                    _unitOfWork.Commit();

                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, $"Internal error: {ex.Message}");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, serviceBase.Notifications);
            }
        }

        public async Task<HttpResponseMessage> ResponseExceptionAsync(Exception ex)
        {
            return Request.CreateResponse(HttpStatusCode.InternalServerError, new { errors = ex.Message, exception = ex.ToString() });
        }

        protected override void Dispose(bool disposing)
        {
            if (_serviceBase != null)
            {
                _serviceBase.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}