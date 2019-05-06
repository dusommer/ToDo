using System.Collections.Generic;
using System.Web.Mvc;
using ToDo.Models.Arguments.ListItem;
using ToDo.ServicesApi;

namespace ToDo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceApiListItem _serviceLisItem;

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
            _serviceLisItem = serviceLisItem;
        }

        public ActionResult Index()
        {
            //UserEmail = "dusommer@gmail.com";
            ViewBag.UserEmail = UserEmail;
            List<ListItemResponse> list = new List<ListItemResponse>();
            if (!string.IsNullOrEmpty(UserEmail))
            {
                list = _serviceLisItem.GetByEmail(UserEmail.ToLower());
            }
            return View(list);
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
            _serviceLisItem.Remove(listId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveList(string name)
        {
            _serviceLisItem.Insert(name, UserEmail);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}