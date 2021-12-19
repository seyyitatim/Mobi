using Mobi.Entities;
using System.ComponentModel.DataAnnotations;

namespace Mobi.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Ürün Adı")]
        public string Name { get; set; }
        [Display(Name = "Kategori Adı")]
        public string CategoryName { get; set; }
        [Display(Name = "Resim Yolu")]
        public string OriginalImagePath { get; set; }
    }
}
