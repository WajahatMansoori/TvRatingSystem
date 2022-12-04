using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaVoirAdmin.Models
{
    public class Category
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int CategoryName { get; set; }
        public int ParentId { get; set; }
    }
}