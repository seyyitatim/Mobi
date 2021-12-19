namespace Mobi.Entities
{
    public class CustomProperty
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }

        public Product Product { get; set; }
        public UserFavorite UserFavorite { get; set; }
    }
}
