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
            children.Add(new Item() { Name = "Nenhum trabalho sobre falsos cognatos pode estar completo sem a presença deste campeão de audiência. O substantivo “PARENT” não significa “parente”", Id = 3, Position = 2 });
            children.Add(new Item() { Name = "Nenhum trabalho sobre falsos cognatos pode estar completo sem a presença deste campeão de audiência. O substantivo “PARENT” não significa “parente”", Id = 3, Position = 2 });
            children.Add(new Item() { Name = "Nenhum trabalho sobre falsos cognatos pode estar completo sem a presença deste campeão de audiência. O substantivo “PARENT” não significa “parente”", Id = 3, Position = 2 });
            children.Add(new Item() { Name = "Nenhum trabalho sobre falsos cognatos pode estar completo sem a presença deste campeão de audiência. O substantivo “PARENT” não significa “parente”", Id = 3, Position = 2 });
            children.Add(new Item() { Name = "Nenhum trabalho sobre falsos cognatos pode estar completo sem a presença deste campeão de audiência. O substantivo “PARENT” não significa “parente”", Id = 3, Position = 2 });
            children.Add(new Item() { Name = "Nenhum trabalho sobre falsos cognatos pode estar completo sem a presença deste campeão de audiência. O substantivo “PARENT” não significa “parente”", Id = 3, Position = 2 });
            children.Add(new Item() { Name = "Nenhum trabalho sobre falsos cognatos pode estar completo sem a presença deste campeão de audiência. O substantivo “PARENT” não significa “parente”", Id = 3, Position = 2 });
            itens.Add(new Item() { Name = "ToDo", Id = 1, Position = 0, Children = children });
            children.RemoveRange(2, 3);
            itens.Add(new Item() { Name = "Doing", Id = 2, Position = 1, Children = children });
            itens.Add(new Item() { Name = "Done", Id = 3, Position = 2 });
            itens.Add(new Item() { Name = "asdfasd", Id = 4, Position = 3 });
            itens.Add(new Item() { Name = "Do sadf asfne", Id = 5, Position = 4 });
            itens.Add(new Item() { Name = "Do asfdew qr23 ne", Id = 6, Position = 5 });
            itens.Add(new Item() { Name = "Do24 234 dfsg ne", Id = 7, Position = 6 });
            itens.Add(new Item() { Name = "sftg werlhkjl,hf", Id = 8, Position = 7 });
            return View(itens);
        }

        public ActionResult Update(string itemIds)
        {
            int count = 1;
            List<int> itemId = new List<int>();
            itemId = itemIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}