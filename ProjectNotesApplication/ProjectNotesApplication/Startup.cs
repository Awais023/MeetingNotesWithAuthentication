using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ProjectNotesApplication.Models;

[assembly: OwinStartupAttribute(typeof(ProjectNotesApplication.Startup))]
namespace ProjectNotesApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();
        }
        public void CreateUserAndRoles()
        { 
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!roleManager.RoleExists("SuperAdmin"))
            {
                var role = new IdentityRole("SuperAdmin");// CREATE SUPER ADMIN ROLE
                roleManager.Create(role);

                var user = new ApplicationUser();//CREATE DEFAULT USER
                user.UserName = "sa@domain.com";
                user.Email = "sa@domain.com";
                string pwd = "Password@2017";
                var newuser = userManager.Create(user, pwd);
                if (newuser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "SuperAdmin");
                }
            }
        }
    }
}

