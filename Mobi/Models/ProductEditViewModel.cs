using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Mobi.Models
{
    public class ProductEditViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Ürün Adı")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        public string SmallImagePath { get; set; }
        public string OriginalImagePath { get; set; }
        [Display(Name = "Resim")]
        public IFormFile Image { get; set; }
    }
}
