using Microsoft.EntityFrameworkCore;
using PetShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopProject.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().HasData(
                new { Id = 1, Name = "Dog", Age = 2.5F, PictureUrl = "dog.jpg", Description = "The dog is a domesticated carnivore of the family Canidae.", CatagoryId = 1 },
                new { Id = 2, Name = "Cat", Age = 3.0F, PictureUrl = "cat.JPG", Description = "The cat (Felis catus) is a domestic species of small carnivorous mammal.", CatagoryId = 1 },
                new { Id = 3, Name = "Giraffe", Age = 3.0F, PictureUrl = "giraffe.jpg", Description = "The giraffe (Giraffa) is an African artiodactyl mammal, the tallest living terrestrial animal and the largest ruminant.", CatagoryId = 1 },
                new { Id = 4, Name = "Snake", Age = 1.0F, PictureUrl = "snake.jpg", Description = "Snakes are elongated, legless, carnivorous reptiles of the suborder Serpentes.", CatagoryId = 3 },
                new { Id = 5, Name = "Turtle", Age = 50F, PictureUrl = "turtle.jpg", Description = "Turtles are reptiles of the order Chelonia or Testudines. They are characterized by a special bony or cartilaginous shell developed from their ribs that acts as a shield.", CatagoryId = 3 },
                new { Id = 6, Name = "Gold Fish", Age = 0.1F, PictureUrl = "goldfish.jpg", Description = "The goldfish (Carassius auratus) is a freshwater fish in the family Cyprinidae of order Cypriniformes. It is commonly kept as a pet in indoor aquariums, and is one of the most popular aquarium fish.", CatagoryId = 2 },
                new { Id = 7, Name = "Parrot", Age = 13F, PictureUrl = "parrot.jpg", Description = "Parrots, also known as psittacines, are birds of the roughly 398 species in 92 genera comprising the order Psittaciformes, found mostly in tropical and subtropical regions.", CatagoryId = 4 }

                );
            modelBuilder.Entity<Catagory>().HasData(
                new { Id = 1, Name = "Mammals" },
                new { Id = 2, Name = "Fish" },
                new { Id = 3, Name = "Reptiles" },
                new { Id = 4, Name = "Birds" }
                );
            modelBuilder.Entity<Comment>().HasData(
                new { Id = 1, AnimalId = 1, CommentText = "I love dogs" },
                new { Id = 2, AnimalId = 1, CommentText = "The dog is a gentleman; I hope to go to his heaven not man's." },
                new { Id = 3, AnimalId = 2, CommentText = "I love cats" },
                new { Id = 4, AnimalId = 2, CommentText = "What greater gift than the love of a cat." },
                new { Id = 5, AnimalId = 2, CommentText = "It is difficult to obtain the friendship of a cat. It is a philosophical animal... one that does not place its affections thoughtlessly." },
                new { Id = 6, AnimalId = 2, CommentText = "My cat knows the meaning of life, but has no interest in sharing the secret." },
                new { Id = 7, AnimalId = 3, CommentText = "Well as giraffes say, you don't get no leaves unless you stick your neck out." },
                new { Id = 8, AnimalId = 3, CommentText = "Evolution is so creative. That's how we got giraffes." },
                new { Id = 9, AnimalId = 4, CommentText = "YAK!" },
                new { Id = 10, AnimalId = 4, CommentText = "Every great story seems to begin with a snake." },
                new { Id = 11, AnimalId = 5, CommentText = "I learn as much from a turtle as from a religious text." },
                new { Id = 12, AnimalId = 5, CommentText = "All the thoughts of a turtle are turtle" },
                new { Id = 13, AnimalId = 5, CommentText = "Anytime you see a turtle up on top of a fence post, you know he had some help." },
                new { Id = 14, AnimalId = 6, CommentText = "I wouldn't mind turning into a vermilion goldfish." },
                new { Id = 15, AnimalId = 6, CommentText = "Goldfish are flowers ... flowers that move." },
                new { Id = 16, AnimalId = 6, CommentText = "A goldfish is reason enough for living, if someone needs a reason." },
                new { Id = 17, AnimalId = 6, CommentText = "Goldfish have no memory, I guess their lives are much like mine. And the little plastic castle is a surprise everytime." },
                new { Id = 18, AnimalId = 6, CommentText = "There is a natural hootchy-kootchy motion to a goldfish." },
                new { Id = 19, AnimalId = 7, CommentText = "Parrots make great pets. They have more personality than goldfish" }
                );
        }
    }
}
