using PetShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopProject.ViewModels
{
    public class AnimalDetailsViewModel
    {
        public Animal Animal { get; set; }
        public Comment Comment { get; set; }
    }
}
