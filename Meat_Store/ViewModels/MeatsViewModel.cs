using Meat_Store.Models;

namespace Meat_Store.ViewModels
{
    public class MeatsViewModel
    {
        public Meat Product { get; set; }
        public string Message { get; set; }
        public IEnumerable<Meat>? meats { get; set; }
    }
}
