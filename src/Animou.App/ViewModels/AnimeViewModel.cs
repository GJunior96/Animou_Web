using System.ComponentModel.DataAnnotations;

namespace Animou.App.ViewModels
{
    public class AnimeViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public int AnimeId { get; set; }
        public Guid UserId { get; set; }
        public int Status { get; set; }
        public UserViewModel User { get; set; }
        public IEnumerable<CommentViewModel>? Comments { get; set; }
    }
}
