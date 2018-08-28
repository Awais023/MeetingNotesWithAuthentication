using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectNotesApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectNotesApplication.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(FormCollection form)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            string userName = form["txtEmail"];
            string email = form["txtEmail"];
            string pwd = form["txtpassword"];

            var user = new ApplicationUser();
            user.UserName = userName;
            user.Email = email;
            var newuser = userManager.Create(user, pwd);
            return View();

        }
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewRole(FormCollection Form)
        {
            String rolename = Form["RoleName"];
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if(!roleManager.RoleExists(rolename))
            {
                //CREATE SUPER ADMIN ROLE
                var role = new IdentityRole(rolename);
                roleManager.Create(role);
            }
            return View("Index");
        }

    }
}