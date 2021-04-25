using PetShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopProject.ViewModels
{
    public class ShowCatalogViewModel
    {
        public IList<Animal> Animals { get; set; }
        public IList<Catagory> Catagories { get; set; }
        
    }

}
