using Microsoft.AspNetCore.Http;
using PetShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopProject.ViewModels
{
    public class EditAnimalViewModel
    {
        public Animal Animal { get; set; }
        public IList<Catagory> Catagories { get; set; }         
        public IFormFile PictureFile { get; set; }
        public string PictureErrorMsg { get; set; }
    }
}
