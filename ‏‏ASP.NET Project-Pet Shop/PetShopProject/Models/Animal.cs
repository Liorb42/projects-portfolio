using PetShopProject.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopProject.Models
{
    public class Animal : IEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter the animal's name")]
        [StringLength(30)]
        public string Name { get; set; }
       
        [Required(ErrorMessage = "Please enter the animal's age")]
        [Range(0, 300)]
        public float Age { get; set; }

        [Display(Name = "Picture")]
        public string PictureUrl { get; set; }

        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public int CatagoryId { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public virtual Catagory Catagory { get; set; }


    }
    public class AnimalComparerByComments : Comparer<Animal>
    {
        public override int Compare([AllowNull] Animal x, [AllowNull] Animal y)
        {
            if (x != null && y != null)
            {
                if (x.Comments != null && y.Comments != null)
                    return x.Comments.Count.CompareTo(y.Comments.Count);
                else if (x.Comments != null) return 1;
                else if (y.Comments != null) return -1;
                else return 0;
            }
            else if (x != null) return 1;
            else if (y != null) return -1;
            else
                return 0;
        }
    }
}
