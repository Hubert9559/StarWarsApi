using StarWars.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarWars.Api.Attributes
{
    public class AreValidEpisodesAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var episodes = value as List<string>;

            foreach(var episode in episodes)
            {
                if (Episodes.IsValid(episode))
                    continue;
                return new ValidationResult($"{episode} is not a valid episode");
            }

            return ValidationResult.Success;
        }
    }
}