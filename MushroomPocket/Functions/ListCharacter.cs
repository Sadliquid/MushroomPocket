using System;
using System.Linq;

namespace MushroomPocket.Functions {
    public static class ListCharactersFunction {
        public static void ListCharacters() {
            using (var context = new DatabaseContext()) {
                context.Database.EnsureCreated();
                var characters = context.Characters.ToList();
                if (characters.Count == 0) {
                    Console.WriteLine("");
                    Console.WriteLine("No characters in your pocket.");
                    context.Dispose();
                    DatabaseManagementFunctions.RemoveTempFiles();
                    return;
                } else {
                    characters = characters.OrderByDescending(c => c.HP).ToList();
                    Console.WriteLine("");
                    foreach (var character in characters) {
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine($"Name: {character.Name}");
                        Console.WriteLine($"HP: {character.HP}");
                        Console.WriteLine($"EXP: {character.EXP}");
                        Console.WriteLine($"Skill: {character.Skill}");
                        Console.WriteLine("-----------------------------");
                    }
                    context.Dispose();
                    DatabaseManagementFunctions.RemoveTempFiles();
                }
            }
        }
    }
}