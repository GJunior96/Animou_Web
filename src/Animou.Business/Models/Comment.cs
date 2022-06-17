namespace Animou.Business.Models
{
    public class Comment : Entity
    {
        public Guid UserId { get; set; }
        public Guid AnimeId { get; set; }
        public string Text { get; set; }

        // Entity Framework Relations
        public User User { get; set; }
        public Anime Anime { get; set; }
        public IEnumerable<Like>? Likes { get; set; }
        public IEnumerable<Deslike>? Deslikes { get; set; }
    }
}
