using System.ComponentModel.DataAnnotations;

namespace Mobi.Models
{
    public class CategoryEditViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Kategori Ad alanı zorunludur.")]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }
    }
}
