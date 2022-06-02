using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmokeLogic;
using SmokeUI.Models;
using System.Collections.Generic;

namespace SmokeUI.Controllers
{
    public class PropertyController : Controller
    {
        private PropertyCollection propertyColl;
        private PropertyHandler propertyHandler;

        public PropertyController(PropertyCollection propertyColl, PropertyHandler propertyHandler)
        {
            this.propertyColl = propertyColl;
            this.propertyHandler = propertyHandler;
        }

        // GET: PropertyController
        public ActionResult Index()
        {
            List<Property> properties = propertyColl.GetAll();
            List<PropertyViewModel> propertyViews = new List<PropertyViewModel>();
            foreach (Property property in properties)
            {
                int propViewId = property.Id;
                int? propViewGameId = property.gameId;
                int? propViewParentId = property.parentId;
                string propViewName = property.name;
                string propViewValue = property.value;
                string propViewType = property.type;
                propertyViews.Add(new PropertyViewModel(propViewId, propViewGameId, propViewParentId, propViewName, propViewValue, propViewType));
            }
            return View(propertyViews);
        }

        // GET: PropertyController/Details/5
        public ActionResult Details(int Id)
        {
            Property property = propertyHandler.GetDetails(Id);
            PropertyViewModel propertyDetails = new PropertyViewModel(property.Id, property.gameId, property.parentId, property.name, property.value, property.type);
            return View(propertyDetails);
        }

        // GET: PropertyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropertyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, int? GameId, int? ParentId, string Name, string Value, string propertyType)
        {
            propertyColl.Add(GameId, ParentId, Name, Value, propertyType);
            return View();
        }

        // GET: PropertyController/Edit/5
        public ActionResult Edit(int ParentId)
        {
            return View();
        }

        // POST: PropertyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, int? GameId, int? ParentId, string Name, string Value, string PropertyType, IFormCollection collection)
        {
            propertyHandler.Update(Id, GameId, ParentId, Name, Value, PropertyType);
            return View();
        }

        // GET: PropertyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PropertyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id, IFormCollection collection)
        {
            propertyColl.Delete(Id);
            return View();
        }
    }
}
