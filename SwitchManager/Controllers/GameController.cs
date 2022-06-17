using SwitchLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SwitchPresentation.Models;
using SwitchInterface;

namespace SwitchPresentation.Controllers
{
    public class GameController : Controller
    {
        private GameCollection gameColl;
        private Game game;

        public GameController(GameCollection gameColl, Game game)
        {
            this.gameColl = gameColl;
            this.game = game;
        }

        // GET: HomeController1
        public ActionResult Index()
        {
            List<GameModel> games = gameColl.GetAllGames();
            List<GameViewModel> gameViews = new List<GameViewModel>();
            foreach (GameModel game in games)
            {
                int gameViewId = game.Id;
                string gameViewName = game.Name;
                string gameViewLocation = game.Location;
                gameViews.Add(new GameViewModel( gameViewId, gameViewName, gameViewLocation));
            }
            return View(gameViews);
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int Id)
        {
            GameModel gameModel = game.GetDetails(Id);
            GameViewModel gameDetails = new GameViewModel( gameModel.Id, gameModel.Name, gameModel.Location );
            return View(gameDetails);
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
            game.UpdateGame(Id, Name, Location);
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            gameColl.DeleteGame(id);
            return View();
        }
    }
}
