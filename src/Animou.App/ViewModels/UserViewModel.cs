using System.ComponentModel.DataAnnotations;

namespace Animou.App.ViewModels
{
    public class UserViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Field {0} must be filled")]
        [StringLength(450, ErrorMessage = "Field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Field {0} must be filled")]
        [EmailAddress(ErrorMessage = "Email provided is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field {0} must be filled")]
        public string Password { get; set; }

        public string? Avatar { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Created { get; set; }

        public int Watched { get; set; }
        public int Watching { get; set; }
        public IEnumerable<CommentViewModel>? Comments { get; set; }
        public IEnumerable<LikeViewModel>? Likes { get; set; }
        public IEnumerable<DeslikeViewModel>? Deslikes { get; set; }
        public IEnumerable<AnimeViewModel>? Animes { get; set; }
    }
}
