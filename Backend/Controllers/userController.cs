using Backend.Interfaces;
using Backend.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUser _iUser;// = new IUser();
        public UserController(IUser iUser)
        {
            _iUser = iUser;
        }
        // GET: userController
        // [NoCache]
        [HttpGet]
        public async Task<List<User>> Get()
        {

            return await _iUser.GetAllUsers();

        }
        [HttpPost]
        [Route("ruta2")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
             await _iUser.AddUSer(user);

            return CreatedAtAction(nameof(Get), new { id = user.id }, user);
        }


        // GET: userController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: userController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: userController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: userController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: userController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: userController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: userController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
