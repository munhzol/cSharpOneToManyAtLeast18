using System;
using System.ComponentModel.DataAnnotations;

namespace ChefNDishes.Models
{
    public class Over18Attribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int age = (int) ((DateTime.Now - (DateTime)value).TotalDays/365.242199);

            if(age<18)
            {
                return new ValidationResult("Chef has to be at least 18!");
            }
            return ValidationResult.Success;
        }
    }
}