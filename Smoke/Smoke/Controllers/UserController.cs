using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmokeLogic;
using SmokeUI.Models;
using System.Collections.Generic;

namespace SmokeUI.Controllers
{
    public class UserController : Controller
    {
        private UserCollection userColl;
        private UserHandler userHandler;

        public UserController(UserCollection propertyColl, UserHandler propertyHandler)
        {
            this.userColl = propertyColl;
            this.userHandler = propertyHandler;
        }
        // GET: UserController
        public ActionResult GetUsers()
        {
            List<User> users = userColl.GetAllUsers();
            List<UserViewModel> userViews = new List<UserViewModel>();

            foreach (User user in users)
            {
                userViews.Add(new UserViewModel(user));
            }
            return View(userViews);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int UserId)
        {
            return View(new UserViewModel(userHandler.GetUserDetails(UserId)));
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(IFormCollection collection, UserViewModel UserViewModel, int GameId)
        {
            User user = new User();
            user.Id = UserViewModel.Id;
            user.Name = UserViewModel.Name;
            user.Email = UserViewModel.Email;
            user.Password = UserViewModel.Password;
            userColl.AddUser(user, GameId);
            return View();
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(int Id, string Name, string Email, string Password, IFormCollection collection)
        {
            userHandler.UpdateUser(Id, Name, Email, Password);
            return View();
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int GameId, int UserId, UserViewModel UserViewModel, IFormCollection collection)
        {
            User user = new User();
            foreach (GameViewModel gameViewModel in UserViewModel.Games)
            {
                user.Games.Add(new Game()
                {
                    Id = gameViewModel.Id,
                    Name = gameViewModel.Name
                });
            }

            userColl.DeleteUser(GameId, UserId, user);
            return View();
        }
    }
}
