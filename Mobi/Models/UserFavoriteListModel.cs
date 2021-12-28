using Mobi.Entities;

namespace Mobi.Models
{
    public class UserFavoriteListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string OriginalImagePath { get; set; }
    }
}
