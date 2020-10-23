using StarWars.Api.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarWars.Api.Resources
{
    public class UpdateCharacterResource
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [AreValidEpisodes]
        public List<string> Episodes { get; set; }
        [AreValidIds]
        public List<long> FriendsIds { get; set; }
    }
}