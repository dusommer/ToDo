using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class HomeController : Controller
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
                var itens = new List<Item>();
                var item4 = new Item() { Description = "ToDo", Id = random.Next(), Position = 0 };
                var item1 = new Item() { Description = "{1635945} [Prior 8] Erro ao Duplicar documento no contas a pagar - fora da data contábil", Id = random.Next(), Position = 0, ParentItemID = item4.Id };
                var item2 = new Item() { Description = "Nenhum trabalho sobre falsos cognatos pode estar completo sem a presença deste campeão de audiência. O substantivo “PARENT” não significa “parente”", Id = random.Next(), Position = 1, ParentItemID = item4.Id };
                var item3 = new Item() { Description = "parent significado, definição parent: 1. a mother or father of a person or an animal: 2. a company that owns one or more other companies: 3. a person who gives", Id = random.Next(), Position = 2, ParentItemID = item4.Id };

                itens.Add(item1);
                itens.Add(item2);
                itens.Add(item3);
                itens.Add(item4);
                itens.Add(new Item() { Description = "Doing", Id = random.Next(), Position = 1 });
                itens.Add(new Item() { Description = "Done", Id = random.Next(), Position = 2 });
                ListItem = itens;
            }
            ViewBag.ListItens = ListItem;
            return View();
        }

        public ActionResult UpdateItem(string itens)
        {
            var updated = new List<Item>();
            if (!string.IsNullOrEmpty(itens))
            {
                var list = itens.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                for (int i = 0; i < list.Length; i++)
                {
                    var item = ListItem.Find(x => x.Id == list[i]);
                    if (item != null && item.Position != i)
                    {
                        item.Position = i;
                        updated.Add(item);
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateSubItem(string itens)
        {
            var updated = new List<Item>();
            if (!string.IsNullOrEmpty(itens))
            {
                var list = itens.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToArray();

                for (int i = 0; i < list.Length; i++)
                {
                    var itemMoved = list[i].Split('|').Select(int.Parse).ToArray();
                    var itemInList = ListItem.Find(x => x.Id == itemMoved[0]);

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

            if (string.IsNullOrEmpty(buttonType) || buttonType.Equals("Save"))
            {
                if (string.IsNullOrEmpty(itemId))
                {
                    item = new Item() { Description = description, Id = random.Next(), Position = ListItem.Count() };

                    if (!string.IsNullOrEmpty(inptParentItemId))
                    {
                        item.ParentItemID = int.Parse(inptParentItemId);
                    }

                    auxList.Add(item);
                }
                else
                {
                    item = auxList.FirstOrDefault(x => x.Id.ToString().Equals(itemId));
                    if (item.ParentItemID != 0 && string.IsNullOrEmpty(inptParentItemId))
                    {
                        item.ParentItemID = 0;
                        item.Position = auxList.Where(x => x.ParentItemID == 0).Max(x => x.Position) + 1;
                    }
                    else if (!string.IsNullOrEmpty(description))
                    {
                        item.Description = description;
                    }
                }
            }
            else
            {
                auxList.RemoveAll(x => x.ParentItemID.ToString().Equals(itemId) || x.Id.ToString().Equals(itemId));
            }

            ListItem = auxList;

            return PartialView("PartialItens", ListItem);
        }
    }
}