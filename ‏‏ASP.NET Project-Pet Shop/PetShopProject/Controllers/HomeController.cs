using Microsoft.AspNetCore.Mvc;
using PetShopProject.Infra;
using PetShopProject.Models;
using PetShopProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetShopProject.Controllers
{
    public class HomeController : Controller
    {
        ICatalogService _catalogService;

        public HomeController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }
        public IActionResult Index()
        {
            List<Animal> animals = _catalogService.GetAllAnimalsSorted().Take(2).ToList();
            return View(animals);
        }
        public IActionResult AnimalDetails(int id)
        {
            AnimalDetailsViewModel viewModel = new AnimalDetailsViewModel
            {
                Animal = _catalogService.GetAnimalById(id)   
            };
                    
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateComment(int animalId, Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.AnimalId = animalId;
                _catalogService.AddCommnet(comment);
                return RedirectToAction("AnimalDetails", new { id = comment.AnimalId });
            }
            else
            {
                AnimalDetailsViewModel viewModel = new AnimalDetailsViewModel
                {
                    Animal = _catalogService.GetAnimalById(comment.AnimalId)
                };
                
                return View("AnimalDetails", viewModel);
            }
        }
        public IActionResult ShowCatalog()
        {
            ShowCatalogViewModel viewModel = new ShowCatalogViewModel
            {
                Catagories = _catalogService.GetAllCatagories().ToList(),
                Animals = _catalogService.GetAllAnimalsSorted().ToList()
            };
            
            return View(viewModel);
        }
        public IActionResult ShowSelection(int id, bool isAdmin)
        {
            List<Catagory> catagories = _catalogService.GetAllCatagories().ToList();
            List<Animal> animals;

            if (id > 0)
                animals = _catalogService.GetAllAnimalsSorted().Where(a => a.CatagoryId == id).ToList();
            else
                animals = _catalogService.GetAllAnimalsSorted().ToList();

            if (isAdmin)
                ViewBag.IsAdminstrator = true;

            return View("_catalogView", animals);
        }
        public IActionResult GetImage(string path)
        {
            path = string.Concat(@"~\Assets\", path); 
            try
            {
                return File(path, "image/jpeg");
            }
            catch (Exception)
            {
                return default;
            }
        }

    }
}
