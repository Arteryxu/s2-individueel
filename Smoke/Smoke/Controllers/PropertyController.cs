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
                propertyViews.Add(new PropertyViewModel(property));
            }
            return View(propertyViews);
        }

        // GET: PropertyController/Details/5
        public ActionResult Details(int PropertyId, int ParentId)
        {
            List<Property> properties = propertyHandler.GetDetails(PropertyId, ParentId);
            List<PropertyViewModel> propertyViews = new List<PropertyViewModel>();

            foreach (Property property in properties)
            {
                propertyViews.Add(new PropertyViewModel(property));
            }

            return View(propertyViews);
        }

        // GET: PropertyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropertyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, Property property)
        {
            propertyColl.Add(property);
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
        public ActionResult Edit(int Id, int GameId, int? ParentId, string Name, string Value, string PropertyType, IFormCollection collection)
        {
            propertyHandler.Update(Id, GameId, ParentId, Name, Value, PropertyType);
            return View();
        }

        // GET: PropertyController/Delete/5
        public ActionResult Delete(int id)
        {
            List<Property> properties = propertyHandler.GetDetails(id, null);
            PropertyViewModel propertyViewModel = new PropertyViewModel(properties[0]);

            return View(propertyViewModel);
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
