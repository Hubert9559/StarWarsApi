using FluentAssertions;
using StarWars.Characters.Domain;
using System;
using System.Collections.Generic;
using Xunit;

namespace StarWars.Characters.Tests
{
    public class Character_Tests
    {
        [Fact]
        public void Character_ShouldBeCreated_ForValidData()
        {
            var name = "valid";
            var episodes = new List<string> { "JEDI", "NEWHOPE" };

            var sut = new Character(name, episodes);

            sut.Should().NotBeNull();
            sut.Name.Should().Be(name);
            sut.Episodes.Should().BeEquivalentTo(episodes);
        }

        [Theory]
        [InlineData(default)]
        [InlineData("")]
        [InlineData("   ")]
        public void Character_ShouldThrowArgumentNullException_ForInvalidName(string name)
        {
            var episodes = new List<string> { "JEDI", "NEWHOPE" };

            Assert.Throws<ArgumentNullException>(() => new Character(name, episodes));
        }

        [Fact]
        public void Character_ShouldThrowArgumentNullException_ForNullEpisodeList()
        {
            var name = "valid";

            Assert.Throws<ArgumentNullException>(() => new Character(name, null));
        }

        [Fact]
        public void Character_ShouldThrowArgumentOutOfRangeException_ForEmptyEpisodeList()
        {
            var name = "valid";

            Assert.Throws<ArgumentOutOfRangeException>(() => new Character(name, new List<string>()));
        }

        [Fact]
        public void Character_ShouldThrowArgumentOutOfRangeException_ForInvalidEpisodeList()
        {
            var name = "valid";

            Assert.Throws<ArgumentOutOfRangeException>(() => new Character(name, new List<string> { "Once Upon a Time in Hollywood" }));
        }
    }
}