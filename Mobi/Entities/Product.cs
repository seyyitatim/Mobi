using System.Collections.Generic;

namespace Mobi.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Model3DPath { get; set; }
        public string SmallImagePath { get; set; }
        public string OriginalImagePath { get; set; }

        public Category Category { get; set; }
        public List<CustomProperty> CustomProperties { get; set; }
        public List<UserFavorite> UserFavorites { get; set; }
    }
}
