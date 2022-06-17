namespace Animou.Business.Models
{
    public class Deslike : Entity
    {
        public Guid UserId { get; set; }
        public Guid CommentId { get; set; }

        // Entity Framework Relations
        public User User { get; set; }
        public Comment Comment { get; set; }
    }
}
