using Microsoft.AspNetCore.Identity;

namespace Animou.Data.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? Avatar { get; set; }
    }
}
