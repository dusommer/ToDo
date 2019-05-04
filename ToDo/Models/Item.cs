using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDo.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public List<Item> Children { get; set; }
    }
}