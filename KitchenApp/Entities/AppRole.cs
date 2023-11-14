using Microsoft.AspNetCore.Identity;

namespace WebApplicationIdentity.Data
{
    public class AppRole : IdentityRole<int>
    {
        public string Description { get; set; } = default!;
    }
}