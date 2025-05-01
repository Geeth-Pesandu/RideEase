using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using RideEase.Models;

public class ApplicationUserManager : UserManager<ApplicationUser>
{
    public ApplicationUserManager(IUserStore<ApplicationUser> store)
        : base(store) { }

    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    {

        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));

        if (!roleManager.RoleExists("Admin"))
        {
            roleManager.Create(new IdentityRole("Admin"));
        }

        var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));

        manager.UserValidator = new UserValidator<ApplicationUser>(manager)
        {
            AllowOnlyAlphanumericUserNames = false,
            RequireUniqueEmail = true
        };

        manager.PasswordValidator = new PasswordValidator
        {
            RequiredLength = 6,
            RequireDigit = true,
            RequireLowercase = true,
            RequireUppercase = true,
            RequireNonLetterOrDigit = false
        };

        return manager;
    }
}
