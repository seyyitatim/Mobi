namespace Mobi.Entities
{
    public class UserFavorite
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int CustomPropertyId { get; set; }

        public AppUser User { get; set; }
        public Product Product { get; set; }
        public CustomProperty CustomProperty { get; set; }
    }
}
