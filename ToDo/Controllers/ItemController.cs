using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using ToDo.Models.Arguments.Item;
using ToDo.ServicesApi;
using ToDo.Utils;

namespace ToDo.Controllers
{
    public class ItemController : Controller
    {
        public int IdList
        {
            get
            {
                return int.Parse(Session["IdList"].ToString());
            }
            set
            {
                Session["IdList"] = value;
            }
        }

        private readonly IServiceApiItem _serviceItem;

        public ItemController(IServiceApiItem serviceItem)
        {
            _serviceItem = serviceItem;
        }

        public ActionResult Index(string id)
        {
            var request = Util.Decrypt(id).Split('|');
            IdList = int.Parse(request[0]);
            ViewBag.ListItems = _serviceItem.GetByLisItem(IdList);
            ViewBag.NomeLista = request[1];
            ViewBag.UserEmail = request[2];
            return View();
        }

        public ActionResult UpdatePositionItem(string items)
        {
            var listItem = _serviceItem.GetByLisItem(IdList);
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

        public ActionResult UpdatePositionSubItem(string items)
        {
            var listItem = _serviceItem.GetByLisItem(IdList);
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
            var auxList = _serviceItem.GetByLisItem(IdList);

            if (buttonType.Equals("Save") || buttonType.Equals("Convert"))
            {
                if (string.IsNullOrEmpty(itemId))
                {
                    SaveItem(description, inptParentItemId, auxList);
                }
                else
                {
                    UpdateItem(description, itemId, buttonType, auxList);
                }
            }
            else
            {
                RemoveItem(itemId, auxList);
            }

            return PartialView("PartialItems", _serviceItem.GetByLisItem(IdList));
        }

        private void RemoveItem(string itemId, System.Collections.Generic.List<ItemResponse> auxList)
        {
            var items = auxList.Where(x => x.IdParentItem.ToString().Equals(itemId));

            foreach (var item in items)
            {
                _serviceItem.Remove(item.Id);
            }

            _serviceItem.Remove(int.Parse(itemId));
        }

        private void UpdateItem(string description, string itemId, string buttonType, System.Collections.Generic.List<ItemResponse> auxList)
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

        private void SaveItem(string description, string inptParentItemId, System.Collections.Generic.List<ItemResponse> auxList)
        {
            var request = new InsertItemRequest() { Description = description, IdListItem = IdList };

            if (!string.IsNullOrEmpty(inptParentItemId))
            {
                request.IdParentItem = int.Parse(inptParentItemId);
                var parentItens = auxList.Where(x => x.IdParentItem != null &&
                                                      x.IdParentItem.Value.ToString() == inptParentItemId);
                request.Position = parentItens.Any() ? parentItens.Max(x => x.Position) + 1 : 0;
            }
            else
            {
                request.Position = auxList.Count > 0 ? auxList.Where(x => x.IdParentItem == null || x.IdParentItem.Value == 0)
                                                             .Max(x => x.Position) + 1 : 0;

            }

            _serviceItem.Insert(request);
        }

        [HttpPost]
        public ActionResult SendEmail(string email, string nomeLista, string userEmail)
        {
            try
            {
                string idEncrypt = Util.Encrypt(IdList.ToString() + "|" + nomeLista + "|" + userEmail);

                string url = string.Format("http://{0}/Item/Index/{1}", HttpContext.Request.Url.Authority, idEncrypt);
                Util.SendEmail(email, nomeLista, userEmail, url);

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, erroMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}