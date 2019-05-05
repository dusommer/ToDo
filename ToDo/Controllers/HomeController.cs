using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var list = new List<ListItem>();
            list.Add(new ListItem() { Id = 1, Name = "Sprint 01/2019 A", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 2, Name = "Sprint 01/2019 B", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 3, Name = "Sprint 02/2019 A", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 4, Name = "Sprint 02/2019 B", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 1, Name = "Sprint 01/2019 A", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 2, Name = "Sprint 01/2019 B", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 3, Name = "Sprint 02/2019 A", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 4, Name = "Sprint 02/2019 B", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 1, Name = "Sprint 01/2019 A", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 2, Name = "Sprint 01/2019 B", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 3, Name = "Sprint 02/2019 A", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 4, Name = "Sprint 02/2019 B", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 1, Name = "Sprint 01/2019 A", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 2, Name = "Sprint 01/2019 B", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 3, Name = "Sprint 02/2019 A", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 4, Name = "Sprint 02/2019 B", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 1, Name = "Sprint 01/2019 A", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 2, Name = "Sprint 01/2019 B", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 3, Name = "Sprint 02/2019 A", UserEmail = "dusommer@hotmail.com" });
            list.Add(new ListItem() { Id = 4, Name = "Sprint 02/2019 B", UserEmail = "dusommer@hotmail.com" });
            return View(list);
        }
    }
}