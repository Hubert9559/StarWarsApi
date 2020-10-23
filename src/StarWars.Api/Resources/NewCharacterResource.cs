using StarWars.Api.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarWars.Api.Resources
{
    public class NewCharacterResource
    {
        [Required]
        public string Name { get; set; }
        [AreValidEpisodes]
        public List<string> Episodes { get; set; }
        [AreValidIds]
        public List<long> FriendsIds { get; set; }
    }
}