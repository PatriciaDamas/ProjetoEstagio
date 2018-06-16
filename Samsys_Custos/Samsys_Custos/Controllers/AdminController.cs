using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using System.Linq;


using System;
using Samsys_Custos.Models;
using Samsys_Custos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Samsys_Custos.Data;

namespace Samsys_Custos.Controllers
{
    //[Authorize(Roles ="SuperAdmin,Gestor")]
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

        public async Task<IActionResult> Index()
        {
       
            var usersWRoles = _userManager.Users.Select(
              n => new ListUsersViewModel
              {
                  Email = n.Email,
                  Id = n.Id,
                  Name = n.Name,
                  id_colaborador=n.id_colaborador,
                  Segsocial = n.Segsocial
                  
                 
              });
            var newusersWRoles = usersWRoles.ToList();
           foreach (var u in newusersWRoles)
            {
                var tusr = await _userManager.FindByIdAsync(u.Id);
                var ru = await _userManager.GetRolesAsync(tusr);
                u.Roles = string.Join(", ", ru);
                
            }

            return View(newusersWRoles);
        }    

        //GET : /Admin/CreateUser
        public async Task<IActionResult> CreateUser()
        {
            ModelState.Clear();

            //var teste = await _roleManager.Roles.ToListAsync();
            CreateUserViewModel vmUser = new CreateUserViewModel
            {
                //RolesList = new SelectList(await _roleManager.Roles.Where(a => a.NormalizedName != "SUPERADMIN").ToListAsync(), "Name", "Name")
                  RolesList = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name")
            };

            return View(vmUser);
        }
       
        //POST : /Admin/CreateUser
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel vmUser)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Name = vmUser.Name,
                    UserName = vmUser.Email,
                    Email = vmUser.Email,
                     id_colaborador = vmUser.id_colaborador,
                      Segsocial = vmUser.Segsocial,      

                  
                  
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
                id_colaborador = user.id_colaborador,
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
                user.id_colaborador = editUserViewModel.id_colaborador;

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