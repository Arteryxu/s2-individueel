﻿using SwitchLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SwitchPresentation.Controllers
{
    public class GameController : Controller
    {
        // GET: HomeController1
        public ActionResult Index()
        {
            GameCollection gameColl = new GameCollection();
            List<Game> games = gameColl.GetAllGames();
            return View(games);
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int Id)
        {
            GameCollection gameColl = new GameCollection();
            Game game = gameColl.GetDetails(Id);
            return View(game);
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, string Name, string Location)
        {
            GameCollection gameColl = new GameCollection();
            gameColl.AddGame(Name, Location);
            return View();
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int Id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection collection, int Id, string Name, string Location)
        {
            GameCollection gameColl = new GameCollection();
            gameColl.UpdateGame(Id, Name, Location);
            return View();
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            GameCollection gameColl = new GameCollection();
            gameColl.DeleteGame(id);
            return View();
        }
    }
}
