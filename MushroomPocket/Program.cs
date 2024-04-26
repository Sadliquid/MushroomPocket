using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using System.Text.RegularExpressions;

namespace MushroomPocket {
    class Program {
        static List<Character> characters = new List<Character>(); // list for storing characters
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
                Console.Write("Please only enter [1, 2, 3, 4] or Q to quit: ");
                string choice = Console.ReadLine().ToUpper(); // so upper and lowercase is accepted

                if (choice == "1") {
                    AddCharacter();
                }
                else if (choice == "2") {
                    ListCharacters();
                }
                else if (choice == "3") {
                    CheckTransform();
                }
                else if (choice == "4") {
                    TransformCharacters(mushroomMasters);
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

        static void AddCharacter() {
            Console.WriteLine("");
            Console.Write("Enter Character's name: ");
            string character = Console.ReadLine().Trim();

            if (character != "Waluigi" && character != "Daisy" && character != "Wario") {
                Console.WriteLine("Invalid character. Please try again."); // if character isnt accepted
                return;
            }

            Console.Write("Enter Character's HP: ");
            int hp;

            if (!int.TryParse(Console.ReadLine(), out hp)) {
                Console.WriteLine("Invalid HP. Please try again."); // in case user doesnt key in an integer
                return;
            }

            if (hp < 0) {
                Console.WriteLine("HP must be positive. Please try again."); // ensure valid HP
                return;
            }

            Console.Write("Enter Character's EXP: ");
            int exp;

            if  (!int.TryParse(Console.ReadLine(), out exp)) {
                Console.WriteLine("Invalid EXP. Please try again."); // in case user doesnt key in an integer
                return;
            }

            if (exp < 0) {
                Console.WriteLine("EXP must be positive. Please try again."); // ensure valid EXP
                return;
            }

            if (character == "Waluigi") {
                characters.Add(new Waluigi() { // add Waluigi to characters list
                    HP = hp,
                    EXP = exp
                });
                Console.WriteLine("");
                Console.WriteLine("Waluigi has been added.");
            }
            else if (character  == "Daisy") {
                characters.Add(new Daisy() { // add Daisy to characters list
                    HP = hp,
                    EXP = exp
                });
                Console.WriteLine("");
                Console.WriteLine("Daisy has been added.");
            }
            else if (character == "Wario") {
                characters.Add(new Wario() { // add Wario to characters list
                    HP = hp,
                    EXP = exp
                });
                Console.WriteLine("");
                Console.WriteLine("Wario has been added.");
            }
        }

        static void ListCharacters() {
            if (characters.Count == 0) {
                Console.WriteLine("");
                Console.WriteLine("No characters in your pocket."); // in case there are currently no characters
                return;
            }

            for (int i = 0; i < characters.Count - 1; i++) {
                for (int j = 0; j < characters.Count - 1 - i; j++) {
                    if (characters[j].HP < characters[j + 1].HP) {
                        Character placeholder = characters[j];
                        characters[j] =  characters[j + 1];
                        characters[j + 1] = placeholder;
                    }
                }
            }

            foreach (var character in characters) {
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"Name: {character.Name}");
                Console.WriteLine($"HP: {character.HP}");
                Console.WriteLine($"EXP: {character.EXP}");
                Console.WriteLine($"Skill: {character.Skill}");
                Console.WriteLine("-----------------------------");
            }
        }

        static void CheckTransform() {
            Console.WriteLine("This feature is not implemented yet");
        }

        static void TransformCharacters(List<MushroomMaster> mushroomMasters) {
            Console.WriteLine("This feature is not implemented yet");
        }
    }

    public abstract class Character // abstract class, avoid direct instances of itself
    {
        public string Name { get; set; } // getter + setter methods for "Name"
        public int HP { get; set; } // getter + setter methods for "HP"
        public int EXP { get; set; } // getter + setter methods for "EXP"
        public string Skill { get; set; } // getter + setter methods for "Skill"
    }

    public class Waluigi : Character // Waluigi's character subclass
    {
        public Waluigi()
        {
            Name = "Waluigi";
            Skill = "Agility";
        }
    }

    public class Daisy : Character // Daisy's character subclass
    {
        public Daisy()
        {
            Name = "Daisy";
            Skill = "Leadership";
        }
    }

    public class Wario : Character // Wario's character subclass
    {
        public Wario()
        {
            Name = "Wario";
            Skill = "Strength";
        }
    }
}
