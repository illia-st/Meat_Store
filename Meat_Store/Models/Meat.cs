using System;
using System.Collections.Generic;

namespace Meat_Store.Models
{
    public partial class Meat
    {
        public Meat()
        {
            FavoutirePositions = new HashSet<FavoutirePosition>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string ShortDesc { get; set; } = null!;
        public string LongDesc { get; set; } = null!;
        public string Img { get; set; } = null!;
        public int Price { get; set; }
        public int Portion { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public int SizeOfPortion { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<FavoutirePosition> FavoutirePositions { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
