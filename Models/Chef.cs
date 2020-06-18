using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChefNDishes.Models
{
    public class Chef
    {
        [Key]
        public int ChefID {get; set;}

        [Required]
        public string FirstName {get; set;}

        [Required]
        public string LastName {get; set;}

        [Required]
        [DataType(DataType.DateTime)]
        // [FutureDate]
        [Over18]
        public DateTime Birthday {get; set;} = DateTime.Now;

        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        public List<Dish> OwnedDishes {get;set;}

    }
}