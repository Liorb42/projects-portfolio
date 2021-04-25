using Microsoft.AspNetCore.Mvc;
using PetShopProject.Infra;
using PetShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SixLabors.ImageSharp;
using PetShopProject.ViewModels;

namespace PetShopProject.Controllers
{
    public class AdministratorController : Controller
    {
        ICatalogService _catalogService;

        public AdministratorController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }
        public IActionResult EditCatalog()
        {
            ShowCatalogViewModel viewModel = new ShowCatalogViewModel
            {
                Catagories = _catalogService.GetAllCatagories().ToList(),
                Animals = _catalogService.GetAllAnimalsSorted().ToList(),
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

            return View("_AdminCatalogView", animals);
        }

        [HttpGet]
        public IActionResult EditAnimal(int id)
        {
            EditAnimalViewModel viewModel = new EditAnimalViewModel
            { 
                Animal = _catalogService.GetAnimalById(id),
                Catagories = _catalogService.GetAllCatagories().ToList(),
            };
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateAnimal(EditAnimalViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                if (viewModel.PictureFile != null)
                {
                    try
                    {
                        using (var image = Image.Load(viewModel.PictureFile.OpenReadStream()))
                        {
                            image.SaveAsJpeg(@$"wwwroot\Assets\{viewModel.PictureFile.FileName}");
                        }
                    }
                    catch (Exception)
                    {
                        viewModel.Catagories = _catalogService.GetAllCatagories().ToList();                        
                        viewModel.PictureErrorMsg = "Invalid file name or file path";
                        return View("EditAnimal", viewModel);
                    }
                    viewModel.Animal.PictureUrl = $"{viewModel.PictureFile.FileName}";                    
                }
                _catalogService.UpdateAnimal(viewModel.Animal.Id, viewModel.Animal);
                return RedirectToAction("AnimalDetails", "Home", new { id = viewModel.Animal.Id });
            }
            else
            {               
                viewModel.Animal = _catalogService.GetAnimalById(viewModel.Animal.Id);
                viewModel.Catagories = _catalogService.GetAllCatagories().ToList();
                return View("EditAnimal", viewModel);
            }
        }
        public IActionResult ConfirmDeleteAnimal(int id)
        {
            Animal animal = _catalogService.GetAnimalById(id);
            return View(animal);
        }
        public IActionResult DeleteAnimal(int id)
        {            
            _catalogService.DeleteAnimalById(id);
            return RedirectToAction("EditCatalog"); 
        }

        [HttpGet]
        public IActionResult AddAnimal()
        {
            EditAnimalViewModel viewModel = new EditAnimalViewModel
            {
                Catagories = _catalogService.GetAllCatagories().ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddAnimalPost(EditAnimalViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.PictureFile != null)
                {
                    try
                    {
                        using (var image = Image.Load(viewModel.PictureFile.OpenReadStream()))
                        {
                            image.SaveAsJpeg(@$"wwwroot\Assets\{viewModel.PictureFile.FileName}");
                        }
                    }
                    catch (Exception)
                    {
                        viewModel.Catagories = _catalogService.GetAllCatagories().ToList();                        
                        viewModel.PictureErrorMsg = "Invalid file name or file path";
                        return View("AddAnimal");
                    }
                    viewModel.Animal.PictureUrl = $"{viewModel.PictureFile.FileName}";
                    viewModel.Animal = _catalogService.AddAnimal(viewModel.Animal);
                    return RedirectToAction("AnimalDetails", "Home", new { id = viewModel.Animal.Id });
                }
                else
                {
                    viewModel.Catagories = _catalogService.GetAllCatagories().ToList();
                    viewModel.PictureErrorMsg = "Invalid file name or file path";
                    return View("AddAnimal");
                }                
            }
            else
            {
                viewModel.Catagories = _catalogService.GetAllCatagories().ToList();
                viewModel.PictureErrorMsg = "Invalid file name or file path";
                return View("AddAnimal");
            }
        }
    }
}
