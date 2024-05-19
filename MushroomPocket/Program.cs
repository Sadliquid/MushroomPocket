using System;
using System.Collections.Generic;
using MushroomPocket.Functions;

namespace MushroomPocket {
    class Program {
        static List<Character> characters = new List<Character>();
        static void Main(string[] args) {   
            List<MushroomMaster> mushroomMasters = new List<MushroomMaster>(){
                new MushroomMaster("Daisy", 2, "Peach"),
                new MushroomMaster("Wario", 3, "Mario"),
                new MushroomMaster("Waluigi", 1, "Luigi")
            };

            while (true) {
                Console.WriteLine("");
                Console.WriteLine("**********************************");
                Console.WriteLine("Welcome to Mushroom Pocket App");
                Console.WriteLine("**********************************");
                Console.WriteLine("(1). Add Mushroom's Character to my pocket");
                Console.WriteLine("(2). List character(s) in my Pocket");
                Console.WriteLine("(3). Check if I can transform my characters");
                Console.WriteLine("(4). Transform characters(s) ");
                Console.WriteLine("(5). Remove a character from my pocket");
                Console.WriteLine("(6). Battle against the PC");
                Console.Write("Please only enter [1, 2, 3, 4, 5, 6] or Q to quit: ");
                string choice = Console.ReadLine().ToUpper();

                if (choice == "1") {
                    AddCharacterFunction.AddCharacter();
                }
                else if (choice == "2") {
                    ListCharactersFunction.ListCharacters();
                }
                else if (choice == "3") {
                    CheckTransformFunction.CheckTransform(mushroomMasters);
                }
                else if (choice == "4") {
                    TransformCharactersFunction.TransformCharacters(mushroomMasters);
                }
                else if (choice == "5") {
                    RemoveCharacterFunction.RemoveCharacter();
                }
                else if (choice == "6") {
                    BattleAgainstPCFunction.BattleAgainstPC();
                }
                else if (choice == "Q") {
                    Console.WriteLine("");
                    Console.WriteLine("MushroomPocket App closed. See you soon!");
                    Console.WriteLine("");
                    Environment.Exit(0);
                }
                else {
                    Console.WriteLine("");
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }
    }
}
