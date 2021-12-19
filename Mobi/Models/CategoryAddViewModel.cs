using System.ComponentModel.DataAnnotations;

namespace Mobi.Models
{
    public class CategoryAddViewModel
    {
        [Required(ErrorMessage ="Kategori Ad alanı zorunludur.")]
        [Display(Name ="Kategori Adı")]
        public string Name { get; set; }
    }
}
