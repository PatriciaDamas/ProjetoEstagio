using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Samsys_Custos.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Samsys_Custos.Helpers;
using System;
using Samsys_Custos.Data;

namespace EmocionAppBackOffice.UI.Controllers
{
    public class AdminController : Controller
    {

        private UserManager<AspNetUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<AspNetUser> userManager, RoleManager<IdentityRole> roleManager)
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
                  Segsocial = n.Segsocial,
                  id_colaborador = n.id_colaborador
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

        // GET : Admin
        public async Task<IActionResult> IndexApp(string searchString, int? page)
        {
            ViewData["CurrentFilter"] = searchString;

            var users = _userManager.Users;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }

            int pageSize = 10;
            return View(await PaginatedList<AspNetUser>.CreateAsync(users.AsNoTracking(), page ?? 1, pageSize));
        }

        //GET : /Admin/CreateUser
        public async Task<IActionResult> CreateUser()
        {
            ModelState.Clear();

            //var teste = await _roleManager.Roles.ToListAsync();
            CreateUserViewModel vmUser = new CreateUserViewModel
            {
                RolesList = new SelectList(await _roleManager.Roles.Where(a => a.NormalizedName != "SUPERADMIN").ToListAsync(), "Name", "Name")
            };

            return View(vmUser);
        }

        //POST : /Admin/CreateUser
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel vmUser)
        {
            if (ModelState.IsValid)
            {
                var user = new AspNetUser
                {
                    Name = vmUser.Name,
                    UserName = vmUser.Email,
                    Email = vmUser.Email,
                    Segsocial = vmUser.Segsocial,
                    id_colaborador = vmUser.id_colaborador
                };

                var result = await _userManager.CreateAsync(user, vmUser.Password);

                if (result.Succeeded)
                {
                    var resultRole = await _userManager.AddToRolesAsync(user, vmUser.SelectedRoles);

                    if (resultRole.Succeeded)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    AddErrors(resultRole);
                }
                AddErrors(result);
            }

            vmUser.RolesList = new SelectList(await _roleManager.Roles.Where(a => a.NormalizedName != "SUPERADMIN").ToListAsync(), "Name", "Name");
            return View(vmUser);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            if (id == null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

         

            EditUserViewModel editUserViewModel = new EditUserViewModel()
            {
                Email = user.Email,
                Name = user.Name,
                id_colaborador=user.id_colaborador,
                Segsocial = user.Segsocial,
                SelectedRoles = (await _userManager.GetRolesAsync(user)).ToArray(),
                RolesList = new SelectList(await _roleManager.Roles.Where(a => a.NormalizedName != "SUPERADMIN").ToListAsync(), "Name", "Name")
            };
            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(editUserViewModel.ID);

                if (user == null)
                    return NotFound();
            

                user.Name = editUserViewModel.Name;
                user.Segsocial = editUserViewModel.Segsocial;

                var userupdateresult = await _userManager.UpdateAsync(user);
                if (userupdateresult.Succeeded)
                {
                    var userRoles = (await _userManager.GetRolesAsync(user));
                    var resultclearroles = await _userManager.RemoveFromRolesAsync(user, userRoles.ToArray());
                    if (resultclearroles.Succeeded)
                    {
                        var resultrole = await _userManager.AddToRolesAsync(user, editUserViewModel.SelectedRoles);
                        if (resultrole.Succeeded)
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                            AddErrors(resultrole);
                    }
                    else
                        AddErrors(resultclearroles);

                }

            }
            editUserViewModel.RolesList = new SelectList(await _roleManager.Roles.Where(a => a.NormalizedName != "SUPERADMIN").ToListAsync(), "Name", "Name");
            return View(editUserViewModel);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.ToString());
            }
        }

        #endregion Helpers

    }
}