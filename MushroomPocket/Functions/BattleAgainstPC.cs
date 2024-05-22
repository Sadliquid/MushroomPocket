using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace MushroomPocket.Functions {
    public static class BattleAgainstPCFunction {

        private static Random randomNumberGenerator = new Random();

        public static string CriticalRoulette() {
            string[] possibilities = { "SUCCESS", "MISSED", "SUCCESS", "MISSED", "SUCCESS", "MISSED" };
            int index = 0;

            Console.WriteLine("");
            Console.WriteLine("You have a chance to deal CRITICAL DMG! Press ENTER to stop!");
            while (!Console.KeyAvailable) {
                index += 1;
                if (index == 6) {
                    index = 0;
                }
                Console.Write($"\r");
                for (int i = 0; i < possibilities.Length; i++) {
                    if (i == index) {
                        Console.Write($"[{possibilities[i]}] ");
                    } else {
                        Console.Write($"- ");
                    }
                }
                Thread.Sleep(100);
            }
            Console.ReadLine();

            return possibilities[index];
        }

        public static int CatchRoulette() {
            int[] probabilities = { 10, 20, 40, 60, 80, 100, 80, 60, 40, 20, 10 };
            int index = 0;

            Console.WriteLine("");
            Console.WriteLine("Roulette is spinning... Press ENTER to stop!");
            while (!Console.KeyAvailable) {
                index += 1;
                if (index == 11) {
                    index = 0;
                }
                Console.Write($"\r");
                for (int i = 0; i < probabilities.Length; i++) {
                    if (i == index) {
                        Console.Write($"[{probabilities[i]}] ");
                    } else {
                        Console.Write($"- ");
                    }
                }
                Thread.Sleep(60);
            }
            Console.ReadLine();

            return probabilities[index];
        }

        public static List<Character> bosses = new List<Character> {
            new Waluigi() {
                HP = 120,
                EXP = 0
            },
            new Luigi() {
                HP = 120,
                EXP = 0
            },
            new Daisy() {
                HP = 120,
                EXP = 0
            },
            new Peach() {
                HP = 120,
                EXP = 0
            },
            new Wario() {
                HP = 120,
                EXP = 0
            },
            new Mario() {
                HP = 120,
                EXP = 0
            }
        };
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

                int opposingStartingHP = 120;
                Console.WriteLine("");
                Console.WriteLine("--------------------------BATTLE MODE--------------------------");
                Console.WriteLine("⬆⬆⬆ Boss battle stats have been boosted! ⬆⬆⬆");
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("");
                for (int i = 0; i < characters.Count; i++) {
                    Console.WriteLine($"({i + 1}). {characters[i].Name}");
                    Console.WriteLine($"HP: {characters[i].HP}");
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
                        int characterHP = selectedPlayingCharacter.HP;

                        bool characterDodgedMove = false;
                        int characterDodgesLeft = 2; // player only has 2 dodges

                        Console.WriteLine("---------------BOSSES---------------");
                        for (int j = 0; j < bosses.Count; j++) {
                            Console.WriteLine($"({j + 1}). {bosses[j].Name}");
                            Console.WriteLine($"HP: {bosses[j].HP}");
                            Console.WriteLine($"Skill: {bosses[j].Skill} ({bosses[j].DMG} DMG)");
                            Console.WriteLine("");
                        }
                        Console.WriteLine("-------------------------------------");

                        Console.Write("Please select your boss opponent: ");
                        string opposingCharacter = Console.ReadLine();
                        Console.WriteLine("-------------------------------------");

                        if (int.TryParse(opposingCharacter, out int convertedOpposingCharacter)) {
                            if ((convertedOpposingCharacter <= bosses.Count) && (convertedOpposingCharacter > 0)) {

                                var selectedOpposingCharacter = bosses[convertedOpposingCharacter - 1];

                                string opposingCharacterName = selectedOpposingCharacter.Name;
                                string opposingCharacterSkill = selectedOpposingCharacter.Skill;
                                int opposingCharacterDMG = selectedOpposingCharacter.DMG;
                                // PC have no dodges

                                Console.WriteLine("");
                                Console.WriteLine("-----------🏁🏁BATTLE START🏁🏁------------");
                                for (int j = 0; j < 3; j++) {
                                    Thread.Sleep(1000);
                                    Console.WriteLine("*************************************");
                                    Console.WriteLine($"Round ({j + 1}/3)");
                                    Console.WriteLine("*************************************");
                                    Thread.Sleep(1500);
                                    Console.WriteLine("---------YOUR TURN---------");
                                    Console.WriteLine($"Your character: {characterName}");
                                    Console.WriteLine($"Your HP: {characterHP}");
                                    Console.WriteLine($"Your skill: {characterSkill} ({characterDMG} DMG)");
                                    Console.WriteLine($"Dodges left: {characterDodgesLeft}");
                                    Console.WriteLine("");
                                    Console.WriteLine($"Boss: {opposingCharacterName}");
                                    Console.WriteLine($"Boss HP: {opposingStartingHP}");
                                    Console.WriteLine($"Boss skill: {opposingCharacterSkill} ({opposingCharacterDMG} DMG)");
                                    Console.WriteLine("---------------------------");
                                    Console.WriteLine("");

                                    while (true) {
                                        Thread.Sleep(500);
                                        Console.Write($"Enter A to attack or D to dodge next opposing attack: ");
                                        string characterMove = Console.ReadLine().ToUpper();
                                        if (characterMove == "A") {
                                            string critical = CriticalRoulette();
                                            if (critical == "SUCCESS") {
                                                opposingStartingHP -= 2 * characterDMG;
                                                Console.WriteLine("");
                                                Console.WriteLine("You dealt CRITICAL (x2) DMG!");
                                                Console.WriteLine($"{2 * characterDMG} damage was dealt to {opposingCharacterName}!");
                                                break;
                                            } else if (critical == "MISSED") {
                                                opposingStartingHP -= characterDMG;
                                                Console.WriteLine("");
                                                Console.WriteLine("You missed your CRITICAL hit!");
                                                Console.WriteLine($"{characterDMG} damage was dealt to {opposingCharacterName}!");
                                                break;
                                            }
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
                                    Thread.Sleep(1500);
                                    Console.WriteLine("");
                                    Console.WriteLine("---------OPPONENT'S TURN---------");
                                    string[] ellipses = { "..", "...", "...." };
                                    for (int wait = 0; wait < 3; wait++) {
                                        Thread.Sleep(1500);
                                        Console.Write($"\rOpponent is making a turn" + ellipses[wait]);
                                    }
                                    if (characterDodgedMove != true) {
                                        characterHP -= opposingCharacterDMG;
                                        Thread.Sleep(1500);
                                        Console.WriteLine($"\rOpposing {opposingCharacterName} dealt {opposingCharacterDMG} damage to you!");
                                    }
                                    else {
                                        Thread.Sleep(1500);
                                        Console.WriteLine($"\rYou dodged {opposingCharacterName}'s attack!");
                                        characterDodgedMove = false;
                                    }
                                    Console.WriteLine("---------------------------------");
                                }
                                Thread.Sleep(1500);
                                Console.WriteLine("");
                                Console.WriteLine("*********RESULTS*********");
                                Console.WriteLine("");
                                Console.WriteLine($"Your {characterName}'s remaining HP: {characterHP}");
                                Console.WriteLine($"Opposing {opposingCharacterName}'s remaining HP: {opposingStartingHP}");
                                if (characterHP > opposingStartingHP) {
                                    Thread.Sleep(1500);
                                    Console.WriteLine("");
                                    Console.WriteLine("🎉🎉 Congratulations! You've won the battle! 🎉🎉");
                                    Thread.Sleep(1500);
                                    selectedPlayingCharacter.EXP += 10;  // gain 10 EXP if u win

                                    int catchChance = CatchRoulette();
                                    Console.WriteLine("");
                                    Console.WriteLine($"Catch chance: {catchChance}%");
                                    Console.WriteLine("");

                                    string[] ellipses = { "..", "...", "...." };
                                    for (int wait = 0; wait < 3; wait++) {
                                        Thread.Sleep(1500);
                                        Console.Write($"\rWait for it" + ellipses[wait]);
                                    }
                                    Thread.Sleep(1500);

                                    int roll = randomNumberGenerator.Next(1, 101);
                                    if (roll <= catchChance) {
                                        if (opposingCharacterName == "Waluigi") {
                                            context.Add(new Waluigi() {
                                                HP = 120,
                                                EXP = 0
                                            });
                                            Console.WriteLine("");
                                            Console.WriteLine($"✅✅ Gotcha! {opposingCharacterName} was caught! ✅✅");
                                            Console.WriteLine($"{opposingCharacterName} has been added to your pocket!");
                                            Thread.Sleep(1500);
                                        } else if (opposingCharacterName == "Luigi") {
                                            context.Add(new Luigi() {
                                                HP = 120,
                                                EXP = 0
                                            });
                                            Console.WriteLine("");
                                            Console.WriteLine($"✅✅ Gotcha! {opposingCharacterName} was caught! ✅✅");
                                            Console.WriteLine($"{opposingCharacterName} has been added to your pocket!");
                                            Thread.Sleep(1500);
                                        } else if (opposingCharacterName == "Daisy") {
                                            context.Add(new Daisy() {
                                                HP = 120,
                                                EXP = 0
                                            });
                                            Console.WriteLine("");
                                            Console.WriteLine($"✅✅ Gotcha! {opposingCharacterName} was caught! ✅✅");
                                            Console.WriteLine($"{opposingCharacterName} has been added to your pocket!");
                                            Thread.Sleep(1500);
                                        } else if (opposingCharacterName == "Peach") {
                                            context.Add(new Peach() {
                                                HP = 120,
                                                EXP = 0
                                            });
                                            Console.WriteLine("");
                                            Console.WriteLine($"✅✅ Gotcha! {opposingCharacterName} was caught! ✅✅");
                                            Console.WriteLine($"{opposingCharacterName} has been added to your pocket!");
                                            Thread.Sleep(1500);
                                        } else if (opposingCharacterName == "Wario") {
                                            context.Add(new Wario() {
                                                HP = 120,
                                                EXP = 0
                                            });
                                            Console.WriteLine("");
                                            Console.WriteLine($"✅✅ Gotcha! {opposingCharacterName} was caught! ✅✅");
                                            Console.WriteLine($"{opposingCharacterName} has been added to your pocket!");
                                            Thread.Sleep(1500);
                                        } else if (opposingCharacterName == "Mario") {
                                            context.Add(new Mario() {
                                                HP = 120,
                                                EXP = 0
                                            });
                                            Console.WriteLine("");
                                            Console.WriteLine($"✅✅ Gotcha! {opposingCharacterName} was caught! ✅✅");
                                            Console.WriteLine($"Boss {opposingCharacterName} has been added to your pocket!");
                                            Thread.Sleep(1500);
                                        } else {
                                            Console.WriteLine("");
                                            Console.WriteLine($"An error occured while catching {opposingCharacterName}.");
                                        }
                                        DatabaseManagementFunctions.UpdateDB(context);
                                        DatabaseManagementFunctions.RemoveTempFiles();
                                    } else {
                                        Console.WriteLine("");
                                        Console.WriteLine($"Oh no! {opposingCharacterName} ran away!");
                                        Console.WriteLine("Better luck next time!");
                                        context.Dispose();
                                        DatabaseManagementFunctions.RemoveTempFiles();
                                    }
                                }
                                else if (opposingStartingHP > characterHP) {
                                    Console.WriteLine("");
                                    Console.WriteLine("The opponent has won the battle. Good game!");
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