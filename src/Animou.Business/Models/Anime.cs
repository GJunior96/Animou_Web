namespace Animou.Business.Models
{
    public class Anime : Entity
    {
        public int AnimeId { get; set; }
        public Guid UserId { get; set; }
        public Status Status { get; set; }

        // Entity Framework Relations
        public User User { get; set; }
        public IEnumerable<Comment>? Comments { get; set; }
    }
}
