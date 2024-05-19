using System;
using System.Linq;

namespace MushroomPocket.Functions {
    public static class BattleAgainstPCFunction {
        public static void BattleAgainstPC() { // Additional comprehensive, creative and useful feature
            using (var context = new DatabaseContext()) {
                context.Database.EnsureCreated();
                var characters = context.Characters.ToList();

                if (characters.Count == 0) {
                    Console.WriteLine("");
                    Console.WriteLine("You don't have any character to battle with!");
                    context.Dispose();
                    DatabaseManagementFunctions.RemoveTempFiles();
                    return;
                }

                int characterStartingHP = 100;
                int opposingStartingHP = 100;
                Console.WriteLine("");
                Console.WriteLine("--------------------------BATTLE MODE--------------------------");
                Console.WriteLine("All characters' HP have been temporarily set to 100 for battle!");
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("");
                for (int i = 0; i < characters.Count; i++) {
                    Console.WriteLine($"({i + 1}). {characters[i].Name}");
                    Console.WriteLine($"HP: {characterStartingHP}");
                    Console.WriteLine($"EXP: {characters[i].EXP}");
                    Console.WriteLine($"Skill: {characters[i].Skill} ({characters[i].DMG} DMG)");
                    Console.WriteLine("");
                }
                Console.WriteLine("-------------------------------------");
                Console.Write("Please choose your character: ");
                string chosenCharacter = Console.ReadLine();

                if (int.TryParse(chosenCharacter, out int convertedChosenCharacter)) { // try convert to integer and assign it to convertedChosenCharacter
                    if ((convertedChosenCharacter <= characters.Count) && (convertedChosenCharacter > 0)) {

                        var selectedPlayingCharacter = characters[convertedChosenCharacter - 1];

                        string characterName = selectedPlayingCharacter.Name;
                        string characterSkill = selectedPlayingCharacter.Skill;
                        int characterDMG = selectedPlayingCharacter.DMG;

                        bool characterDodgedMove = false;
                        int characterDodgesLeft = 2; // player only has 2 dodges

                        Console.Write("Please choose your opponent: ");
                        string opposingCharacter = Console.ReadLine();
                        Console.WriteLine("-------------------------------------");

                        if (int.TryParse(opposingCharacter, out int convertedOpposingCharacter)) {
                            if ((convertedOpposingCharacter <= characters.Count) && (convertedOpposingCharacter > 0)) {

                                var selectedOpposingCharacter = characters[convertedOpposingCharacter - 1];

                                string opposingCharacterName = selectedOpposingCharacter.Name;
                                string opposingCharacterSkill = selectedOpposingCharacter.Skill;
                                int opposingCharacterDMG = selectedOpposingCharacter.DMG;
                                // PC have no dodges

                                Console.WriteLine("");
                                Console.WriteLine("-------------BATTLE START------------");
                                for (int j = 0; j < 3; j++) {
                                    Console.WriteLine("*************************************");
                                    Console.WriteLine($"Round ({j + 1}/3)");
                                    Console.WriteLine("*************************************");
                                    Console.WriteLine("---------YOUR TURN---------");
                                    Console.WriteLine($"Your character: {characterName}");
                                    Console.WriteLine($"Your HP: {characterStartingHP}");
                                    Console.WriteLine($"Your skill: {characterSkill} ({characterDMG} DMG)");
                                    Console.WriteLine($"Dodges left: {characterDodgesLeft}");
                                    Console.WriteLine("");
                                    Console.WriteLine($"Opposing character: {opposingCharacterName}");
                                    Console.WriteLine($"Opposing HP: {opposingStartingHP}");
                                    Console.WriteLine($"Opposing skill: {opposingCharacterSkill} ({opposingCharacterDMG} DMG)");
                                    Console.WriteLine("---------------------------");
                                    Console.WriteLine("");

                                    while (true) {
                                        Console.Write($"Enter A to attack or D to dodge next opposing attack: ");
                                        string characterMove = Console.ReadLine().ToUpper();
                                        if (characterMove == "A") {
                                            opposingStartingHP -=  characterDMG;
                                            Console.WriteLine("");
                                            Console.WriteLine($"You dealt {characterDMG} damage to {opposingCharacterName}!");
                                            break;
                                        }
                                        else if (characterMove == "D") {
                                            if (characterDodgesLeft > 0) {
                                                characterDodgedMove = true;
                                                characterDodgesLeft -= 1;
                                                break;
                                            }
                                            else {
                                                Console.WriteLine("");
                                                Console.WriteLine("You've run out of dodges!");
                                                Console.WriteLine("");
                                            }
                                        }
                                        if (characterMove != "A" && characterMove != "D") {
                                            Console.WriteLine("");
                                            Console.WriteLine("Invalid input!");
                                            Console.WriteLine("");
                                        }
                                    }
                                    Console.WriteLine("");
                                    Console.WriteLine("---------OPPONENT'S TURN---------");
                                    if (characterDodgedMove != true) {
                                        characterStartingHP -= opposingCharacterDMG;
                                        Console.WriteLine($"Opposing {opposingCharacterName} dealt {opposingCharacterDMG} damage to you!");
                                    }
                                    else {
                                        Console.WriteLine($"You dodged {opposingCharacterName}'s attack!");
                                        characterDodgedMove = false;
                                    }
                                    Console.WriteLine("---------------------------------");
                                }
                                Console.WriteLine("");
                                Console.WriteLine("*********RESULTS*********");
                                Console.WriteLine("");
                                Console.WriteLine($"Your {characterName}'s remaining HP: {characterStartingHP}");
                                Console.WriteLine($"Opposing {opposingCharacterName}'s remaining HP: {opposingStartingHP}");
                                if (characterStartingHP > opposingStartingHP) {
                                    Console.WriteLine("");
                                    Console.WriteLine("Congratulations! You've won the battle!");
                                    selectedPlayingCharacter.EXP += 10;  // gain 10 EXP if u win

                                    DatabaseManagementFunctions.UpdateDB(context);
                                    DatabaseManagementFunctions.RemoveTempFiles();
                                }
                                else if (opposingStartingHP > characterStartingHP) {
                                    Console.WriteLine("");
                                    Console.WriteLine("The opponent has won the battle. Better luck next time!");
                                }
                                else {
                                    Console.WriteLine("");
                                    Console.WriteLine("This battle was a draw! Fair match and well played!");
                                }
                                context.Dispose();
                                DatabaseManagementFunctions.RemoveTempFiles();
                            }
                            else {
                                Console.WriteLine("");
                                Console.WriteLine("No such character!");
                                context.Dispose();
                                DatabaseManagementFunctions.RemoveTempFiles();
                                return;
                            }
                        }
                        else {
                            Console.WriteLine("");
                            Console.WriteLine("Please enter a valid integer.");
                            context.Dispose();
                            DatabaseManagementFunctions.RemoveTempFiles();
                            return;
                        }
                    }
                    else {
                        Console.WriteLine("");
                        Console.WriteLine("No such character!");
                        context.Dispose();
                        DatabaseManagementFunctions.RemoveTempFiles();
                        return;
                    }
                }
                else {
                    Console.WriteLine("");
                    Console.WriteLine("Please enter a valid integer.");
                    context.Dispose();
                    DatabaseManagementFunctions.RemoveTempFiles();
                    return;
                }
            }
        }
    }
}