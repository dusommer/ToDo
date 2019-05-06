using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using ToDo.Models;
using ToDo.Models.Arguments.Item;
using ToDo.ServicesApi;

namespace ToDo.Controllers
{
    public class ItemController : Controller
    {
        public int idList = 15;
        private readonly IServiceApiItem _serviceItem;

        public ItemController(IServiceApiItem serviceItem)
        {
            _serviceItem = serviceItem;
        }
        public ActionResult Index()
        {
            ViewBag.ListItems = _serviceItem.GetByLisItem(idList);
            return View();
        }

        public ActionResult UpdateItem(string items)
        {
            var listItem = _serviceItem.GetByLisItem(idList);
            if (!string.IsNullOrEmpty(items))
            {
                var list = items.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                for (int i = 0; i < list.Length; i++)
                {
                    var item = listItem.Find(x => x.Id == list[i]);
                    if (item != null && item.Position != i)
                    {
                        item.Position = i;

                        _serviceItem.Update((UpdateItemRequest)item);
                    }
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateSubItem(string items)
        {
            var listItem = _serviceItem.GetByLisItem(idList);
            if (!string.IsNullOrEmpty(items))
            {
                var list = items.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToArray();

                for (int i = 0; i < list.Length; i++)
                {
                    var itemMoved = list[i].Split('|').Select(int.Parse).ToArray();
                    var itemInList = listItem.Find(x => x.Id == itemMoved[0]);

                    if (itemInList != null &&
                        (itemInList.Position != i || (itemInList.Position == i && itemInList.IdParentItem != itemMoved[1])))
                    {
                        itemInList.Position = i;
                        itemInList.IdParentItem = itemMoved[1];

                        _serviceItem.Update((UpdateItemRequest)itemInList);
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SaveItem(string description, string itemId, string inptParentItemId, string buttonType)
        {
            var auxList = _serviceItem.GetByLisItem(idList);

            if (buttonType.Equals("Save") || buttonType.Equals("Convert"))
            {
                if (string.IsNullOrEmpty(itemId))
                {
                    var request = new InsertItemRequest() { Description = description, IdListItem = idList };

                    if (!string.IsNullOrEmpty(inptParentItemId))
                    {
                        request.IdParentItem = int.Parse(inptParentItemId);
                        var parentItens = auxList.Where(x => x.IdParentItem != null &&
                                                              x.IdParentItem.Value.ToString() == inptParentItemId);
                        request.Position = parentItens.Count() > 0 ? parentItens.Max(x => x.Position) + 1 : 0;
                    }
                    else
                    {
                        request.Position = auxList.Count > 0 ? auxList.Where(x => x.IdParentItem == null || x.IdParentItem.Value == 0)
                                                                     .Max(x => x.Position) + 1 : 0;

                    }

                    _serviceItem.Insert(request);
                }
                else
                {
                    var item = auxList.FirstOrDefault(x => x.Id.ToString().Equals(itemId));
                    item.Description = description;

                    if (buttonType.Equals("Convert") && item.IdParentItem != 0)
                    {
                        item.IdParentItem = null;
                        item.Position = auxList.Count > 0 ? auxList.Where(x => x.IdParentItem == null || x.IdParentItem.Value == 0).Max(x => x.Position) + 1 : 0;
                    }
                    _serviceItem.Update((UpdateItemRequest)item);
                }
            }
            else
            {
                var items = auxList.Where(x => x.IdParentItem.ToString().Equals(itemId));

                foreach (var item in items)
                {
                    _serviceItem.Remove(item.Id);
                }

                _serviceItem.Remove(int.Parse(itemId));
            }

            return PartialView("PartialItems", _serviceItem.GetByLisItem(idList));
        }

        [HttpPost]
        public ActionResult SendEmail(string email)
        {
            String FROM = "vribbraprojectodo@gmail.com";
            String FROMNAME = "ToDo Project";
            String TO = "dusommer@gmail.com";
            String SMTP_USERNAME = "vribbraprojectodo@gmail.com";
            String SMTP_PASSWORD = "@Todoproject";
            String CONFIGSET = "ConfigSet";
            String HOST = "smtp.gmail.com";
            int PORT = 587;
            String SUBJECT =
                "Amazon SES test (SMTP interface accessed using C#)";
            String BODY =
                "<h1>Amazon SES Test</h1>" +
                "<p>This email was sent through the " +
                "<a href='https://aws.amazon.com/ses'>Amazon SES</a> SMTP interface " +
                "using the .NET System.Net.Mail library.</p>";
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(FROM, FROMNAME);
            message.To.Add(new MailAddress(TO));
            message.Subject = SUBJECT;
            message.Body = BODY;
            message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

            using (var client = new System.Net.Mail.SmtpClient(HOST, PORT))
            {
                client.Credentials =
                    new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

                client.EnableSsl = true;

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                }
            }
            return PartialView("PartialItems", _serviceItem.GetByLisItem(idList));
        }
    }
}