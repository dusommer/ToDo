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
        public List<Item> ListItem
        {
            get
            {
                if (Session["ListItem"] != null)
                {
                    return (Session["ListItem"] as List<Item>).OrderBy(x => x.Position).ToList();
                }
                return new List<Item>();
            }
            set
            {
                Session["ListItem"] = value;
            }
        }
        public ActionResult Index()
        {
            if (ListItem.Count == 0)
            {
                List<Item> itens = new List<Item>();
                List<Item> children = new List<Item>();
                children.Add(new Item() { Description = "{1635945} [Prior 8] Erro ao Duplicar documento no contas a pagar - fora da data contábil", Id = 1, Position = 0 });
                children.Add(new Item() { Description = "parent significado, definição parent: 1. a mother or father of a person or an animal: 2. a company that owns one or more other companies: 3. a person who gives", Id = 2, Position = 1 });
                children.Add(new Item() { Description = "Nenhum trabalho sobre falsos cognatos pode estar completo sem a presença deste campeão de audiência. O substantivo “PARENT” não significa “parente”", Id = 3, Position = 2 });
                itens.Add(new Item() { Description = "ToDo", Id = 1, Position = 0, Children = children });
                itens.Add(new Item() { Description = "Doing", Id = 2, Position = 1, Children = children });
                itens.Add(new Item() { Description = "Done", Id = 3, Position = 2 });
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

        public ActionResult UpdateSubItem(string itemIds)
        {
            int count = 1;
            List<int> itemId = new List<int>();
            //itemId = itemIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SaveItem(string description, string itemId, string buttonType)
        {
            var auxList = ListItem;
            var item = new Item();
            switch (buttonType)
            {
                case "Save":
                    item = new Item() { Description = description, Id = ListItem.Max(x=>x.Id) + 1, Position = ListItem.Count() };
                    auxList.Add(item);
                    break;
                case "Update":
                    item = auxList.FirstOrDefault(x => x.Id.ToString().Equals(itemId));
                    item.Description = description;
                    break;
                case "Remove":
                    item = auxList.FirstOrDefault(x => x.Id.ToString().Equals(itemId));
                    auxList.Remove(item);
                    break;
                default:
                    break;
            }
            ListItem = auxList;
            return PartialView("PartialItens", ListItem);
        }
    }
}