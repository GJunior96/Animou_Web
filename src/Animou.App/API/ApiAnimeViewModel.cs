namespace Animou.App.API
{
    public class ApiAnimeViewModel
    {
        public int Id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? status { get; set; }
        public string? coverImage { get; set; }
        public string? bannerImage { get; set; }
        public string? format { get; set; }
        public int score { get; set; }
        public string? episodes { get; set; }
        public IEnumerable<string>? studios { get; set; }
        public IEnumerable<string>? genres { get; set; }
        public bool? onList { get; set; } = false;
        
        public ApiAnimeViewModel ShallowCopy()
        {
            return (ApiAnimeViewModel)this.MemberwiseClone();
        }
    }
}
