using Meat_Store.Models;
using System.Collections.Generic;

namespace Meat_Store.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Meat> ?All_Products { get; set; }
    }
}
