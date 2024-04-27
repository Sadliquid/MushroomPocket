using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using System.Text.RegularExpressions;

namespace MushroomPocket {
    class Program {
        static List<Character> characters = new List<Character>(); // list for storing characters
        static bool noCharactersToTransform = true;
        static bool noEligibleTransformations = true;
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
                string choice = Console.ReadLine().ToUpper(); // so upper and lowercase is accepted

                if (choice == "1") {
                    AddCharacter();
                }
                else if (choice == "2") {
                    ListCharacters();
                }
                else if (choice == "3") {
                    CheckTransform(mushroomMasters);
                }
                else if (choice == "4") {
                    TransformCharacters(mushroomMasters);
                }
                else if (choice == "5") {
                    RemoveCharacter();
                }
                else if (choice == "6") {
                    BattleAgainstPC();
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
                Console.WriteLine("");
                Console.WriteLine("Invalid HP. Please try again."); // in case user doesnt key in an integer
                return;
            }

            if (hp < 0) {
                Console.WriteLine("");
                Console.WriteLine("HP must be positive. Please try again."); // ensure valid HP
                return;
            }

            Console.Write("Enter Character's EXP: ");
            int exp;

            if  (!int.TryParse(Console.ReadLine(), out exp)) {
                Console.WriteLine("");
                Console.WriteLine("Invalid EXP. Please try again."); // in case user doesnt key in an integer
                return;
            }

            if (exp < 0) {
                Console.WriteLine("");
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
                    if (characters[j].HP < characters[j + 1].HP) { // if previous character HP < next character HP
                        Character placeholder = characters[j]; // create variable to store HP of previous character
                        characters[j] =  characters[j + 1]; // swap positions
                        characters[j + 1] = placeholder; // assign placeholder HP to next character (after sorting)
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

        static void CheckTransform(List<MushroomMaster> mushroomMasters) {
            noCharactersToTransform = true;
            List<string> eligibleTransformations = new List<string>();
            if (characters.Count == 0) {
                Console.WriteLine("");
                Console.WriteLine("No characters in your pocket.");
                return;
            }

            int index = 0;
            Console.WriteLine("");
            foreach (var master in mushroomMasters) { // loop thru the given mushroomMasters list
                if (index == 0) {
                    int daisyCount = characters.Count(c => c.Name == master.Name); // count number of characters where name is "Daisy"
                    if  (daisyCount >= 2) {
                        noCharactersToTransform = false;
                        int eligibleDaisys = daisyCount / master.NoToTransform; // floor divide
                        for (int i = 0; i < eligibleDaisys; i++) {
                            Console.WriteLine(master.Name + " --> " + master.TransformTo);
                        }
                    }
                    index += 1;
                }

                else if (index == 1) {
                    int warioCount = characters.Count(c => c.Name == master.Name); // count number of characters where name is "Wario"
                    if (warioCount >= 3) {
                        noCharactersToTransform = false;
                        int eligibleWarios = warioCount / master.NoToTransform; // floor divide
                        for (int i = 0; i < eligibleWarios; i++) {
                            Console.WriteLine(master.Name + " --> " + master.TransformTo);
                        }
                    }
                    index += 1;
                }
                
                else if (index == 2) {
                    int waluigiCount = characters.Count(c => c.Name == master.Name); // count number of characters where name is "Waluigi"
                    if (waluigiCount >= 1) {
                        noCharactersToTransform = false;
                        int eligibleWaluigis = waluigiCount / master.NoToTransform; // floor divide
                        for (int i = 0; i < eligibleWaluigis; i++) {
                            Console.WriteLine(master.Name + " --> " + master.TransformTo);
                        }
                    }
                    index += 1;
                }
            }

            if (noCharactersToTransform == true) {
                Console.WriteLine("No characters to transform.");
            }
        }

        static void TransformCharacters(List<MushroomMaster> mushroomMasters) {
            noEligibleTransformations = true;
            if (characters.Count == 0) {
                Console.WriteLine("No characters in your pocket.");
                return;
            }

            int index = 0;
            Console.WriteLine("");
            foreach (var master in mushroomMasters) {
                if (index == 0) {
                    int daisyCount = characters.Count(c => c.Name == master.Name);
                    if (daisyCount >= 2) {
                        noEligibleTransformations = false;
                        int eligibleDaisys = daisyCount / master.NoToTransform;
                        int daisysToRemove = eligibleDaisys * master.NoToTransform;
                        int daisysRemoved = 0;
                        for (int i = characters.Count() - 1; i >= 0; i--) { // loop thru from the back to avoid conflict of indexes at the front
                            if (characters[i].Name == master.Name) {
                                characters.RemoveAt(i); // remove the character from characters list
                                daisysRemoved += 1;
                                if (daisysRemoved == daisysToRemove) {
                                    break; // so we dont remove more than needed
                                }
                            }
                        }

                        for (int j = 0; j < eligibleDaisys; j++) {
                            characters.Add(new Peach { // add transformed character
                                HP = 100,
                                EXP = 0
                            });

                            Console.WriteLine(master.Name + " has been transformed to " + master.TransformTo);
                        }
                    }
                    index += 1;
                }

                else if (index == 1) {
                    int warioCount = characters.Count(c => c.Name == master.Name);
                    if (warioCount >= 3) {
                        noEligibleTransformations = false;
                        int eligibleWarios = warioCount / master.NoToTransform;
                        int wariosToRemove = eligibleWarios * master.NoToTransform;
                        int wariosRemoved = 0;
                        for (int i = characters.Count() - 1; i >= 0; i--) {
                            if (characters[i].Name == master.Name) {
                                characters.RemoveAt(i);
                                wariosRemoved += 1;
                                if (wariosRemoved == wariosToRemove) {
                                    break;
                                }
                            }
                        }

                        for (int j = 0; j < eligibleWarios; j++) {
                            characters.Add(new Mario {
                                HP = 100,
                                EXP = 0
                            });

                            Console.WriteLine(master.Name + " has been transformed to " + master.TransformTo);
                        }
                    }
                    index += 1;
                }
                
                else if (index == 2) {
                    int waluigiCount = characters.Count(c => c.Name == master.Name);
                    if (waluigiCount >= 1) {
                        noEligibleTransformations = false;
                        int eligibleWaluigis = waluigiCount / master.NoToTransform;
                        int waluigisToRemove = eligibleWaluigis * master.NoToTransform;
                        int waluigisRemoved = 0;
                        for (int i = characters.Count() - 1; i >= 0; i--) {
                            if (characters[i].Name == master.Name) {
                                characters.RemoveAt(i);
                                waluigisRemoved += 1;
                                if (waluigisRemoved == waluigisToRemove) {
                                    break;
                                }
                            }
                        }

                        for (int j = 0; j < eligibleWaluigis; j++) {
                            characters.Add(new Luigi {
                                HP = 100,
                                EXP = 0
                            });

                            Console.WriteLine(master.Name + " has been transformed to " + master.TransformTo);
                        }
                    }
                    index += 1;
                }
            }

            if (noEligibleTransformations == true) {
                Console.WriteLine("Not enough characters to transform!");
            }
        }

        static void RemoveCharacter(){ // Additional simple useful feature
            if (characters.Count == 0) {
                Console.WriteLine("No characters in your pocket.");
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
            Console.Write("Enter the  number of the character you want to remove: ");

            string characterToDelete = Console.ReadLine();

            try {
                int convertedCharacterToDelete = int.Parse(characterToDelete);
                if (convertedCharacterToDelete > characters.Count()) {
                    Console.WriteLine("The chosen character does not exist in your pocket.");
                    return;
                }

                string characterName = characters[int.Parse(characterToDelete) - 1].Name;

                characters.RemoveAt(int.Parse(characterToDelete) - 1);
                Console.WriteLine("");
                Console.WriteLine(characterName + "  has been removed from your pocket.");
            }

            catch {
                Console.WriteLine("Please enter a valid integer.");
                return;
            }
        }

        static void BattleAgainstPC() {
            if (characters.Count == 0) {
                Console.WriteLine("");
                Console.WriteLine("You don't have any character to battle with!");
                return;
            }
            Console.WriteLine("-------------BATTLE MODE-------------");
            Console.WriteLine("-------------------------------------");
            for (int i = 0; i < characters.Count; i++) {
                Console.WriteLine($"({i + 1}). {characters[i].Name}");
                Console.WriteLine($"HP: {characters[i].HP}");
                Console.WriteLine($"EXP: {characters[i].EXP}");
                Console.WriteLine($"Skill: {characters[i].Skill}");
                Console.WriteLine("");
            }
            Console.WriteLine("-------------------------------------");
            Console.Write("Please choose your character: ");
            string chosenCharacter = Console.ReadLine();

            if (int.TryParse(chosenCharacter, out int convertedChosenCharacter)) { // try converting to integer and assign it to convertedChosenCharacter
                if ((convertedChosenCharacter <= characters.Count) && (convertedChosenCharacter > 0)) {
                    string characterName = characters[convertedChosenCharacter - 1].Name;
                    int characterHP = characters[convertedChosenCharacter - 1].HP;
                    string characterSkill = characters[convertedChosenCharacter - 1].Skill;
                    int characterDMG = characters[convertedChosenCharacter - 1].DMG;
                    bool characterDodgedMove = false;
                    int characterDodgesLeft = 2; // player only has 2 dodges

                    Console.Write("Please choose your opponent: ");
                    string opposingCharacter = Console.ReadLine();

                    if (int.TryParse(opposingCharacter, out int convertedOpposingCharacter)) {
                        if ((convertedOpposingCharacter <= characters.Count) && (convertedOpposingCharacter > 0)) {
                            string opposingCharacterName = characters[convertedOpposingCharacter - 1].Name;
                            int opposingCharacterHP = characters[convertedOpposingCharacter - 1].HP;
                            string opposingCharacterSkill = characters[convertedOpposingCharacter - 1].Skill;
                            int opposingCharacterDMG = characters[convertedOpposingCharacter - 1].DMG;
                            // PC will have no dodges

                            Console.WriteLine("-------------BATTLE START-------------");
                            for (int j = 0; j < 3; j++) {
                                Console.WriteLine("**********************************************");
                                Console.WriteLine($"Round ({j + 1}/3)");
                                Console.WriteLine("**********************************************");
                                Console.WriteLine("---------YOUR TURN---------");
                                Console.WriteLine($"Your character: {characterName}");
                                Console.WriteLine($"Your HP: {characterHP}");
                                Console.WriteLine($"Your skill: {characterSkill} ({characterDMG} DMG)");
                                Console.WriteLine($"Dodges left: {characterDodgesLeft}");
                                Console.WriteLine("");
                                Console.WriteLine($"Opposing character: {opposingCharacterName}");
                                Console.WriteLine($"Opposing HP: {opposingCharacterHP}");
                                Console.WriteLine($"Opposing skill: {opposingCharacterSkill} ({opposingCharacterDMG} DMG)");
                                Console.WriteLine("---------------------------");
                                Console.WriteLine("");

                                while (true) {
                                    Console.Write($"Enter A to attack or D to dodge next opposing attack: ");
                                    string characterMove = Console.ReadLine().ToUpper();
                                    if (characterMove == "A") {
                                        opposingCharacterHP -=  characterDMG;
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
                                    characterHP -= opposingCharacterDMG;
                                    Console.WriteLine($"Opposing {opposingCharacterName} dealt {opposingCharacterDMG} damage to you!");
                                }
                                else {
                                    Console.WriteLine($"You dodged {opposingCharacterName}'s attack!");
                                    characterDodgedMove = false;
                                }
                                Console.WriteLine("--------------------------------");
                            }
                            Console.WriteLine("");
                            Console.WriteLine("*********RESULTS*********");
                            Console.WriteLine("");
                            Console.WriteLine($"Your {characterName}'s remaining HP: {characterHP}");
                            Console.WriteLine($"Opposing {opposingCharacterName}'s remaining HP: {opposingCharacterHP}");
                            if (characterHP > opposingCharacterHP) {
                                Console.WriteLine("");
                                Console.WriteLine("Congratulations! You've won the battle!");
                            }
                            else if (opposingCharacterHP > characterHP) {
                                Console.WriteLine("");
                                Console.WriteLine("The opponent has won the battle. Better luck next time!");
                            }
                            else {
                                Console.WriteLine("");
                                Console.WriteLine("This battle was a draw! Fair match and well played!");
                            }
                        }
                        else {
                            Console.WriteLine("");
                            Console.WriteLine("No such character!");
                            return;
                        }
                    }
                    else {
                        Console.WriteLine("");
                        Console.WriteLine("Please enter a valid integer.");
                        return;
                    }
                }
                else {
                    Console.WriteLine("");
                    Console.WriteLine("No such character!");
                    return;
                }
            }
            else {
                Console.WriteLine("");
                Console.WriteLine("Please enter a valid integer.");
                return;
            }
        }
    }

    public abstract class Character { // abstract class, avoid direct instances of itself
        public string Name { get; set; } // getter + setter methods for "Name"
        public int HP { get; set; } // getter + setter methods for "HP"
        public int EXP { get; set; } // getter + setter methods for "EXP"
        public string Skill { get; set; } // getter + setter methods for "Skill"
        public int DMG { get; set; } // getter + setter methods for "DMG"
    }

    public class Waluigi : Character { // Waluigi's character subclass
        public Waluigi()
        {
            Name = "Waluigi";
            Skill = "Agility";
            DMG = 21;
        }
    }

    public class Daisy : Character { // Daisy's character subclass
        public Daisy()
        {
            Name = "Daisy";
            Skill = "Leadership";
            DMG = 20;
        }
    }

    public class Wario : Character { // Wario's character subclass
        public Wario()
        {
            Name = "Wario";
            Skill = "Strength";
            DMG = 25;
        }
    }

    public class Peach : Character { // Peach's character subclass
        public Peach()
        {
            Name = "Peach";
            Skill = "Magic Abilities";
            DMG = 22;
        }
    }

    public class Mario : Character { // Mario's character subclass
        public Mario()
        {
            Name = "Mario";
            Skill = "Combat Skills";
            DMG = 30;
        }
    }

    public class Luigi : Character { // Luigi's character subclass
        public Luigi()
        {
            Name = "Luigi";
            Skill = "Precision and Accuracy";
            DMG = 18;
        }
    }
}
