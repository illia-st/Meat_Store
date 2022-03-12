using System;
using System.Collections.Generic;

namespace Meat_Store.Models
{
    public partial class Category
    {
        public Category()
        {
            Meats = new HashSet<Meat>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<Meat> Meats { get; set; }
    }
}
