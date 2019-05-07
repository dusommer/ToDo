using System.Collections.Generic;
using System.Web.Mvc;
using ToDo.Models.Arguments.ListItem;
using ToDo.ServicesApi;

namespace ToDo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceApiListItem _serviceListItem;

        public string UserEmail
        {
            get
            {
                if (Session["ListItem"] == null)
                    return "";

                return Session["ListItem"].ToString();
            }
            set
            {
                Session["ListItem"] = value;
            }
        }

        public HomeController(IServiceApiListItem serviceLisItem)
        {
            _serviceListItem = serviceLisItem;
        }

        public ActionResult Index()
        {
            ViewBag.UserEmail = UserEmail;
            List<ListItemResponse> list = new List<ListItemResponse>();
            if (!string.IsNullOrEmpty(UserEmail))
            {
                list = _serviceListItem.GetByEmail(UserEmail.ToLower());
            }
            return View(list);
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnterEmail(string inptEmail)
        {
            UserEmail = inptEmail;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveList(string listId)
        {
            _serviceListItem.Remove(listId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveList(string name)
        {
            _serviceListItem.Insert(name, UserEmail);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}