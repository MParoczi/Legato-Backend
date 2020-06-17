using Microsoft.AspNetCore.Identity;

namespace Legato.Models.UserModels
{
    /// <summary>
    ///     Represents an application role that can be requested by (and granted to) a client application, or that can be used
    ///     to assign an application to users or groups in a specified role.
    /// </summary>
    public class AppRole : IdentityRole<int>
    {
    }
}