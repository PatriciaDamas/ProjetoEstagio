using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;
using Samsys_Custos.Data;
using Samsys_Custos.ViewModels;
using Samsys_Custos.Helpers;

namespace Samsys_Custos.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        // GET : Admin

        public async Task<IActionResult> Index(string searchString, int? page)
        {
            int pageSize = 10;
            ViewData["CurrentFilter"] = searchString;

            //TODO : IMPROVE BAD WORKAROUND
            // 1UP => ApplicationRole:IdentityRole IMP
            var _usersFG = await _userManager.GetUsersInRoleAsync("RGPD");
            var _usersSA = await _userManager.GetUsersInRoleAsync("SuperAdmin");
            var usersWRoles = _userManager.Users.Where(usr => !_usersFG.Contains(usr) && !_usersSA.Contains(usr)).Select(
              n => new ListUsersViewModel
              {
                  Email = n.Email,
                  Id = n.Id,
                  Name = n.Name,
                  id_colaborador = n.id_colaborador,
                  Segsocial = n.Segsocial
              });

            var count = usersWRoles.Count();
            var users = await usersWRoles.Skip(((page ?? 1) - 1) * pageSize).Take(pageSize).ToListAsync();

            foreach (var u in users)
            {
                var tusr = await _userManager.FindByIdAsync(u.Id);
                var ru = await _userManager.GetRolesAsync(tusr);
                u.Roles = string.Join(", ", ru);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).ToList();
                count = users.Count();
            }

            //TODO: Alterar o método de passagem de dados para as páginas


            return View(new PaginatedList<ListUsersViewModel>(users, count, page ?? 1, pageSize));
            //return View(await PaginatedList<ListUsersViewModel>.CreateAsync(usersWRoles.AsNoTracking(), page ?? 1, pageSize));
        }
    }
}