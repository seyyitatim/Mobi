using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Mobi.Models
{
    public class ProductAddViewModel
    {
        [Required]
        [Display(Name ="Ürün Adı")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Kategori")]
        public int CategoryId { get; set; }
        [Display(Name = "Resim")]
        public IFormFile Image { get; set; }
    }
}
