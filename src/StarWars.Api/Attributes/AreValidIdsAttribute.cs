using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarWars.Api.Attributes
{
    public class AreValidIdsAttribute : ValidationAttribute 
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is List<long> ids))
                return new ValidationResult("Not valid Ids");
            foreach (var id in ids)
            {
                if (id > 0)
                    continue;
                return new ValidationResult($"{id} is not a valid id");
            }

            return ValidationResult.Success;
        }
    }
}