using System.ComponentModel.DataAnnotations;

namespace Animou.App.ViewModels
{
    public class CommentViewModel
    {
        [Key]
        public string Id { get; set; }
        
        [Required (ErrorMessage = "You must put on some content in the comment field")]
        [StringLength (450, ErrorMessage = "A comment accepts a maximum of 450 characters")]
        public string Text { get; set; }

        public UserViewModel User { get; set; }
        public AnimeViewModel Anime { get; set; }
        public IEnumerable<LikeViewModel>? Likes { get; set; }
        public IEnumerable<DeslikeViewModel>? Deslikes { get; set; }
    }
}
