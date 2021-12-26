namespace Mobi.Entities
{
    public class UserRoom
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RoomName { get; set; }
        public string Data { get; set; }
        public AppUser User { get; set; }
    }
}
