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
        public ActionResult UserIndex()
        {
            List<User> users = userColl.GetAllUsers();
            List<UserViewModel> userViews = new List<UserViewModel>();

            foreach (User user in users)
            {
                userViews.Add(new UserViewModel(user));
            } 
            return View(userViews);
        }

        public ActionResult UserGameIndex()
        {
            List<User> users = userColl.GetAllUserGames();
            List<UserViewModel> userViews = new List<UserViewModel>();

            foreach (User user in users)
            {
                userViews.Add(new UserViewModel(user));
            }
            return View(userViews);
        }
        public ActionResult MyUserGameIndex(int UserId)
        {
            List<User> users = userColl.GetUserGames(UserId);
            List<UserViewModel> userViews = new List<UserViewModel>();

            foreach (User user in users)
            {
                userViews.Add(new UserViewModel(user));
            }
            return View(userViews);
        }

        // GET: UserController/Details/5
        public ActionResult UserDetails(int UserId)
        {
            return View(new UserViewModel(userHandler.GetUserDetails(UserId)));
        }

        public ActionResult UserGameDetails(int UserId, int GameId)
        {
            return View(new UserViewModel(userHandler.GetUserGameDetails(UserId, GameId)));
        }

        // GET: UserController/Create
        public ActionResult AddUser()
        {
            return View();
        }

        public ActionResult AddUserGame()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(IFormCollection collection, int id, string name, string email, string password)
        {
            User user = new User();
            user.Id = id;
            user.Name = name;
            user.Email = email;
            user.Password = password;
            userColl.AddUser(user);
            List<UserViewModel> users = new List<UserViewModel>();
            foreach(var item in userColl.GetAllUsers())
            {
                users.Add(new UserViewModel(item));
            }
            return RedirectToAction(nameof(UserIndex));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserGame(IFormCollection collection, User User, int GameId)
        {
            userColl.AddUserGame(GameId, User.Id);
            return RedirectToAction(nameof(UserGameIndex));
        }

        // GET: UserController/Edit/5
        public ActionResult EditUser(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(int Id, string Name, string Email, string Password, IFormCollection collection)
        {
            userHandler.UpdateUser(Id, Name, Email, Password);
            return RedirectToAction(nameof(UserIndex));
        }

        public ActionResult DeleteUser([FromQuery]int UserId)
        {
            return View(new UserViewModel(userHandler.GetUserDetails(UserId)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser([FromForm] int UserId, IFormCollection collection)
        {
            userColl.DeleteUser(UserId);
            return RedirectToAction(nameof(UserIndex));
        }

        public ActionResult DeleteUserGame([FromQuery] int UserId, [FromQuery] int GameId)
        {
            return View(new UserViewModel(userHandler.GetUserGameDetails(GameId, UserId)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserGame([FromForm] int UserId, [FromForm] int GameId, IFormCollection collection)
        {
            userColl.DeleteUserGame(GameId, UserId);
            return RedirectToAction(nameof(UserGameIndex));
        }
    }
}
