using System;

namespace MushroomPocket.Functions {
    public static class AddCharacterFunction {
        public static void AddCharacter() {
            Console.WriteLine("");
            Console.Write("Enter Character's name: ");
            string character = Console.ReadLine().Trim();

            if (character != "Waluigi" && character != "Daisy" && character != "Wario") {
                Console.WriteLine("");
                Console.WriteLine("Invalid character. Please try again.");
                return;
            }

            Console.Write("Enter Character's HP (Max: 100): ");
            int hp;

            if (!int.TryParse(Console.ReadLine(), out hp)) {
                Console.WriteLine("");
                Console.WriteLine("Invalid HP. Please try again.");
                return;
            }

            if (hp <= 0) {
                Console.WriteLine("");
                Console.WriteLine("HP must be more than 0. Please try again."); // ensure valid HP
                return;
            }

            if (hp > 100) {
                Console.WriteLine("");
                Console.WriteLine("Max HP allowed is 100. Please try again.");
                return;
            }

            Console.Write("Enter Character's EXP: ");
            int exp;

            if (!int.TryParse(Console.ReadLine(), out exp)) {
                Console.WriteLine("");
                Console.WriteLine("Invalid EXP. Please try again.");
                return;
            }

            if (exp < 0) {
                Console.WriteLine("");
                Console.WriteLine("EXP must be positive. Please try again."); // ensure valid EXP
                return;
            }

            try {
                using (var context = new DatabaseContext()) {
                    Character characterToAdd = null;
                    if (character == "Waluigi") {
                        characterToAdd = new Waluigi() {
                            HP = hp,
                            EXP = exp
                        };
                    }
                    else if (character == "Daisy") {
                        characterToAdd = new Daisy() {
                            HP = hp,
                            EXP = exp
                        };
                    }
                    else if (character == "Wario") {
                        characterToAdd = new Wario() {
                            HP = hp,
                            EXP = exp
                        };
                    }

                    if (characterToAdd != null) {
                        context.Database.EnsureCreated();
                        context.Characters.Add(characterToAdd);

                        DatabaseManagementFunctions.UpdateDB(context);

                        Console.WriteLine("");
                        Console.WriteLine(character + " has been added!");
                        DatabaseManagementFunctions.RemoveTempFiles();
                    } else {
                        Console.WriteLine("");
                        Console.WriteLine("Invalid character. Please try again.");
                        context.Dispose();
                        DatabaseManagementFunctions.RemoveTempFiles();
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