using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class ItemController : Controller
    {
        Random random = new Random();
        public List<Item> ListItem
        {
            get
            {
                List<Item> retorno = null;
                if (Session["ListItem"] != null)
                {
                    retorno = (Session["ListItem"] as List<Item>).OrderBy(x => x.Position).ToList();
                }
                return retorno;
            }
            set
            {
                Session["ListItem"] = value;
            }
        }
        public ActionResult Index()
        {
            if (ListItem == null)
            {
                var items = new List<Item>();
                var item4 = new Item() { Description = "ToDo", ID = random.Next(), Position = 0 };
                var item1 = new Item() { Description = "{1635945} [Prior 8] Erro ao Duplicar documento no contas a pagar - fora da data contábil", ID = random.Next(), Position = 0, ParentItemID = item4.ID };
                var item2 = new Item() { Description = "Nenhum trabalho sobre falsos cognatos pode estar completo sem a presença deste campeão de audiência. O substantivo “PARENT” não significa “parente”", ID = random.Next(), Position = 1, ParentItemID = item4.ID };
                var item3 = new Item() { Description = "parent significado, definição parent: 1. a mother or father of a person or an animal: 2. a company that owns one or more other companies: 3. a person who gives", ID = random.Next(), Position = 2, ParentItemID = item4.ID };

                items.Add(item1);
                items.Add(item2);
                items.Add(item3);
                items.Add(item4);
                items.Add(new Item() { Description = "Doing", ID = random.Next(), Position = 1 });
                items.Add(new Item() { Description = "Done", ID = random.Next(), Position = 2 });
                ListItem = items;
            }
            ViewBag.ListItems = ListItem;
            return View();
        }

        public ActionResult UpdateItem(string items)
        {
            var updated = new List<Item>();
            if (!string.IsNullOrEmpty(items))
            {
                var list = items.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                for (int i = 0; i < list.Length; i++)
                {
                    var item = ListItem.Find(x => x.ID == list[i]);
                    if (item != null && item.Position != i)
                    {
                        item.Position = i;
                        updated.Add(item);
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateSubItem(string items)
        {
            var updated = new List<Item>();
            if (!string.IsNullOrEmpty(items))
            {
                var list = items.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToArray();

                for (int i = 0; i < list.Length; i++)
                {
                    var itemMoved = list[i].Split('|').Select(int.Parse).ToArray();
                    var itemInList = ListItem.Find(x => x.ID == itemMoved[0]);

                    if (itemInList != null &&
                        (itemInList.Position != i || (itemInList.Position == i && itemInList.ParentItemID == itemMoved[1])))
                    {
                        itemInList.Position = i;
                        itemInList.ParentItemID = itemMoved[1];
                        updated.Add(itemInList);

                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SaveItem(string description, string itemId, string inptParentItemId, string buttonType)
        {
            var auxList = ListItem;
            var item = new Item();

            if (buttonType.Equals("Save") || buttonType.Equals("Convert"))
            {
                if (string.IsNullOrEmpty(itemId))
                {
                    item = new Item() { Description = description, ID = random.Next(), Position = ListItem.Count() };

                    if (!string.IsNullOrEmpty(inptParentItemId))
                    {
                        item.ParentItemID = int.Parse(inptParentItemId);
                    }

                    auxList.Add(item);
                }
                else
                {
                    item = auxList.FirstOrDefault(x => x.ID.ToString().Equals(itemId));
                    item.Description = description;

                    if (buttonType.Equals("Convert") && item.ParentItemID != 0)
                    {
                        item.ParentItemID = 0;
                        item.Position = auxList.Where(x => x.ParentItemID == 0).Max(x => x.Position) + 1;
                    }
                }
            }
            else
            {
                auxList.RemoveAll(x => x.ParentItemID.ToString().Equals(itemId) || x.ID.ToString().Equals(itemId));
            }

            ListItem = auxList;

            return PartialView("PartialItems", ListItem);
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
            return PartialView("PartialItems", ListItem);
        }
    }
}