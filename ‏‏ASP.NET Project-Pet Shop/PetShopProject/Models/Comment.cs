using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PetShopProject.Infra;


namespace PetShopProject.Models
{
    public class Comment : IEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }       

        [Required (ErrorMessage ="A comment cannot be empty")]
        [StringLength(1000)]
        public string CommentText { get; set; }

        [Required]
        public int AnimalId { get; set; }
        public virtual Animal Animal { get; set; }
    }
}
