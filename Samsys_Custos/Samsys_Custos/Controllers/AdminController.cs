using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;
using Samsys_Custos.Data;

namespace Samsys_Custos.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AspNetUsers> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<AspNetUsers> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}