﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace SalesTracker.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ItemController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Item item)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            _db.Items.Add(item);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
            return View(thisItem);
            
        }

        public IActionResult Edit (Item item)
        {
            _db.Update(item);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
            return View(thisItem);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
            _db.Remove(thisItem);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult TotalInventory()
        {
            var ItemList = _db.Items;
            return Json(ItemList);
        }

        public IActionResult NewSale()
        {
            var ItemList = _db.Items;
            return Json(ItemList);
        }


        public IActionResult SalesForm2()
        {
            var itemsList = _db.Items.ToList();
            return View(itemsList);
        }

        [HttpPost]
        public async Task<IActionResult> SalesForm2(int stuff)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);

            List<string> itemCount = Request.Form["itemCount"].ToList();
            List<string> itemId = Request.Form["itemId"].ToList();
            List<string> itemPrice = Request.Form["itemPrice"].ToList();
            List<string> itemCost = Request.Form["itemCost"].ToList();
            string [] comment = Request.Form["comment"];
            var stupidThing = new Item { };
            var thisThing = stupidThing.SalesInfo(itemCount, itemId, itemPrice, itemCost, comment);
            Sale newSale = new Sale(thisThing);
            newSale.User = currentUser;
            _db.Sales.Add(newSale);
            _db.SaveChanges();
            return RedirectToAction("Index", "Item", Json(newSale));
        }


    }
}
