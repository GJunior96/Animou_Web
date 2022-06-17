namespace Animou.Business.Models
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Avatar { get; set; }
        public DateTime Created { get; set; }
        public int Watched { get; set; }
        public int Watching { get; set; }

        // Entity Framework Relations
        public IEnumerable<Comment>? Comments { get; set; }
        public IEnumerable<Like>? Likes { get; set; }
        public IEnumerable<Deslike>? Deslikes { get; set; }
        public IEnumerable<Anime>? Animes { get; set; }

    }
}
