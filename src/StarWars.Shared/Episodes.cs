using System;
using System.Linq;

namespace StarWars.Shared
{
    public static class Episodes
    {
        //Original trilogy names
        public static readonly string[] Names = { "NEWHOPE", "EMPIRE", "JEDI" };
        public static bool IsValid(string name) => Names.Contains(name);
    }
}