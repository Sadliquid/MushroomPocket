using System;
using System.Linq;

namespace MushroomPocket.Functions {
    public static class RemoveCharacterFunction {
        public static void RemoveCharacter(){ // Additional simple useful feature
            try {
                using (var context = new DatabaseContext()) {
                    context.Database.EnsureCreated();
                    var characters = context.Characters.ToList();

                    if (characters.Count == 0) {
                        Console.WriteLine("");
                        Console.WriteLine("No characters in your pocket to remove!");
                        context.Dispose();
                        DatabaseManagementFunctions.RemoveTempFiles();
                        return;
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Characters in your pocket:");
                    Console.WriteLine("-----------------------------");
                    for (int i = 0; i < characters.Count; i++) {
                        Console.WriteLine($"({i + 1}). {characters[i].Name}");
                        Console.WriteLine($"HP: {characters[i].HP}");
                        Console.WriteLine($"EXP: {characters[i].EXP}");
                        Console.WriteLine($"Skill: {characters[i].Skill}");
                        Console.WriteLine("");
                    }
                    Console.WriteLine("-----------------------------");
                    Console.Write("Enter the number of the character you want to remove: ");

                    string characterToDelete = Console.ReadLine();

                    try {
                        int convertedCharacterToDelete = int.Parse(characterToDelete);
                        if ((convertedCharacterToDelete > characters.Count()) || (convertedCharacterToDelete <= 0)) {
                            Console.WriteLine("");
                            Console.WriteLine("No such character!");
                            context.Dispose();
                            DatabaseManagementFunctions.RemoveTempFiles();
                            return;
                        }

                        string characterName = characters[convertedCharacterToDelete - 1].Name;

                        context.Characters.Remove(characters[convertedCharacterToDelete - 1]);

                        DatabaseManagementFunctions.UpdateDB(context);
                        DatabaseManagementFunctions.RemoveTempFiles();

                        Console.WriteLine("");
                        Console.WriteLine(characterName + "  has been removed from your pocket.");
                    }

                    catch {
                        Console.WriteLine("");
                        Console.WriteLine("Please enter a valid integer.");
                        return;
                    }
                }
            }

            catch {
                Console.WriteLine("");
                Console.WriteLine("An error occurred while adding the character. Most likely a database connection issue.");
            }
        }
    }
}