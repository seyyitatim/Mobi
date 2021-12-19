using System.ComponentModel.DataAnnotations;

namespace Mobi.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Ürün Adı")]
        public string Name { get; set; }
    }
}
