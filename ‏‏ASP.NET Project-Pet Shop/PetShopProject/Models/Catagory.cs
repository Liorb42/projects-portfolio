using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PetShopProject.Infra;


namespace PetShopProject.Models
{
    public class Catagory : IEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public ICollection<Animal> Animals { get; set; }

    }
}
