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
        public ActionResult Index()
        {
            List<Item> itens = new List<Item>();
            List<Item> children = new List<Item>();
            children.Add(new Item() { Name = "{1635945} [Prior 8] Erro ao Duplicar documento no contas a pagar - fora da data contábil", Id = 1, Position = 0 });
            children.Add(new Item() { Name = "parent significado, definição parent: 1. a mother or father of a person or an animal: 2. a company that owns one or more other companies: 3. a person who gives", Id = 2, Position = 1 });
            children.Add(new Item() { Name = "Nenhum trabalho sobre falsos cognatos pode estar completo sem a presença deste campeão de audiência. O substantivo “PARENT” não significa “parente”", Id = 3, Position = 2 });
            itens.Add(new Item() { Name = "ToDo", Id = 1, Position = 0, Children = children });
            itens.Add(new Item() { Name = "Doing", Id = 2, Position = 1, Children = children });
            itens.Add(new Item() { Name = "Done", Id = 3, Position = 2 });
            return View(itens);
        }

        public ActionResult UpdateItem(string itemIds)
        {
            int count = 1;
            List<int> itemId = new List<int>();
            itemId = itemIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateSubItem(string itemIds)
        {
            int count = 1;
            List<int> itemId = new List<int>();
            itemId = itemIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}