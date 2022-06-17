using System.ComponentModel.DataAnnotations;

namespace Animou.App.ViewModels
{
    public class LikeViewModel
    {
        [Key]
        public string Id { get; set; }

        public UserViewModel User { get; set; }
        public CommentViewModel Comment { get; set; }
    }
}
