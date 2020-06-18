using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChefNDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefNDishes.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Chef> chefs = dbContext.Chefs.Include(
                c => c.OwnedDishes
            ).ToList();
            return View("Index", chefs);
        }

        [HttpGet("/chef/{cmd}/{chefID}")]
        public IActionResult ChefForm(string cmd,int chefID)
        {
            Chef chef = new Chef();

            switch(cmd){
                case "edit": 
                    chef = (Chef)dbContext.Chefs.FirstOrDefault(c => c.ChefID == chefID);
                    return View("ChefForm", chef);
                case "add":
                default: return View("ChefForm", chef);
            }
        }


        [HttpPost("/chef/save/{chefID}")]
        public IActionResult SaveChef(int chefID, Chef AChef)
        {
            if(ModelState.IsValid)
                {
                    if(chefID==0){
                        dbContext.Add(AChef);
                    } else {
                        
                        Chef chef = dbContext.Chefs.FirstOrDefault(c => c.ChefID == chefID);
                        
                        chef.FirstName = AChef.FirstName;
                        chef.LastName = AChef.LastName;
                        chef.Birthday = AChef.Birthday;
                        chef.UpdatedAt = DateTime.Now;

                    }

                    dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    // Oh no!  We need to return a ViewResponse to preserve the ModelState, and the errors it now contains!
                    return View("ChefForm", AChef);
                }      
        }

        
        public HomeController(MyContext context)
        {
            dbContext = context;
        }


        [HttpGet("dishes")]
        public IActionResult Dishes()
        {
            List<Dish> dishes = dbContext.Dishes.Include(
                d => d.Owner
            ).ToList();
            return View("Dishes", dishes);
        }

        [HttpGet("dish/{cmd}/{dishID}")]
        public IActionResult Dishes(string cmd,int dishID)
        {

            Dish dish = new Dish();

            switch(cmd){
                case "edit": 
                    dish = (Dish)dbContext.Dishes.FirstOrDefault(d => d.DishID == dishID);
                    return View("DishForm", dish);
                case "add":
                default: 
                    ViewBag.Chefs = dbContext.Chefs.ToList();
                    return View("DishForm", dish);
            }
        }
        

        [HttpPost("/dish/save/{dishID}")]
        public IActionResult SaveDish(int dishID, Dish ADish)
        {
            if(ModelState.IsValid)
                {
                    if(dishID==0){
                        dbContext.Add(ADish);
                    } else {
                        
                        Dish dish = dbContext.Dishes.FirstOrDefault(d => d.DishID == dishID);
                        
                        dish.Name = ADish.Name;
                        dish.ChefID = ADish.ChefID;
                        dish.Calories = ADish.Calories;
                        dish.Tastiness = ADish.Tastiness;
                        dish.UpdatedAt = DateTime.Now;

                    }

                    dbContext.SaveChanges();
                    return RedirectToAction("Dishes");
                }
                else
                {
                    ViewBag.Chefs = dbContext.Chefs.ToList();
                    return View("DishForm", ADish);
                }      
        }


    }
}
