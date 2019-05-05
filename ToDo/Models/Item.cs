using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDo.Models
{
    public class Item
    {
        public int ID { get; set; }
        public int ListID { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public int ParentItemID { get; set; }
    }
}