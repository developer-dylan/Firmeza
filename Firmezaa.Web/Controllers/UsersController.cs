using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Firmezaa.Web.Models.Entities;
using Firmezaa.Web.Services.Interfaces;
using Firmezaa.Web.ViewModels.Users;

namespace Firmezaa.Web.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class UsersController : Controller
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _service.GetAllAsync();
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _service.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                DocumentNumber = model.DocumentNumber,
                PhoneNumber = model.Phone,
                RegisterDate = DateTime.UtcNow,
                EmailConfirmed = true
            };

            try
            {
                await _service.CreateWithPasswordAsync(user, model.Password, "Client");

                TempData["Success"] = "User created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error creating user.");
                return View(model);
            }
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _service.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, User user)
        {
            if (id != user.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(user);

            try
            {
                await _service.UpdateAsync(user);

                TempData["Success"] = "User updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _service.ExistsAsync(user.Id))
                    return NotFound();

                throw;
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error updating user.");
                return View(user);
            }
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _service.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _service.DeleteAsync(id);

                TempData["Success"] = "User deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error deleting user.");

                var user = await _service.GetByIdAsync(id);
                return View(user);
            }
        }
    }
}
