using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MushroomPocket {
    class Program {
        static List<Character> characters = new List<Character>();
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
                string choice = Console.ReadLine().ToUpper();

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

        static void UpdateDB(DatabaseContext context) {
            context.SaveChanges();
            context.Database.ExecuteSqlInterpolated($"PRAGMA wal_checkpoint(FULL)");
            context.Dispose();
        }

        static void RemoveTempFiles() {
            if (File.Exists("database.db-shm")) {  // remove to always ensure a clean database state
                File.Delete("database.db-shm");
            }
            if (File.Exists("database.db-wal")) { // also remove this to always ensure a clean database state
                File.Delete("database.db-wal");
            }
        }

        static void AddCharacter() {
            Console.WriteLine("");
            Console.Write("Enter Character's name: ");
            string character = Console.ReadLine().Trim();

            if (character != "Waluigi" && character != "Daisy" && character != "Wario") {
                Console.WriteLine("");
                Console.WriteLine("Invalid character. Please try again.");
                return;
            }

            Console.Write("Enter Character's HP: ");
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
                        UpdateDB(context);
                        Console.WriteLine("");
                        Console.WriteLine(character + " has been added!");
                        RemoveTempFiles();
                    } else {
                        Console.WriteLine("");
                        Console.WriteLine("Invalid character. Please try again.");
                        context.Dispose();
                        RemoveTempFiles();
                    }
                }
            }

            catch {
                Console.WriteLine("");
                Console.WriteLine("An error occurred while adding the character. Most likely a database connection issue.");
            }
        }

        static void ListCharacters() {
            using (var context = new DatabaseContext()) {
                context.Database.EnsureCreated();
                var characters = context.Characters.ToList();
                if (characters.Count == 0) {
                    Console.WriteLine("");
                    Console.WriteLine("No characters in your pocket.");
                    context.Dispose();
                    RemoveTempFiles();
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
                    RemoveTempFiles();
                }
            }
        }

        static void CheckTransform(List<MushroomMaster> mushroomMasters) {
            noCharactersToTransform = true;
            List<string> eligibleTransformations = new List<string>();
            using (var context = new DatabaseContext()) {
                context.Database.EnsureCreated();
                var characters = context.Characters.ToList();

                if (characters.Count == 0) {
                    Console.WriteLine("");
                    Console.WriteLine("No characters in your pocket.");
                    context.Dispose();
                    RemoveTempFiles();
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
                        int warioCount = characters.Count(c => c.Name == master.Name);
                        if (warioCount >= 3) {
                            noCharactersToTransform = false;
                            int eligibleWarios = warioCount / master.NoToTransform;
                            for (int i = 0; i < eligibleWarios; i++) {
                                Console.WriteLine(master.Name + " --> " + master.TransformTo);
                            }
                        }
                        index += 1;
                    }
                    
                    else if (index == 2) {
                        int waluigiCount = characters.Count(c => c.Name == master.Name);
                        if (waluigiCount >= 1) {
                            noCharactersToTransform = false;
                            int eligibleWaluigis = waluigiCount / master.NoToTransform;
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
                context.Dispose();
                RemoveTempFiles();
            }
        }

        static void TransformCharacters(List<MushroomMaster> mushroomMasters) {
            noEligibleTransformations = true;
            try {
                using (var context = new DatabaseContext()) {
                    context.Database.EnsureCreated();
                    var characters = context.Characters.ToList();

                    if (characters.Count == 0) {
                        Console.WriteLine("");
                        Console.WriteLine("No characters in your pocket.");
                        context.Dispose();
                        RemoveTempFiles();
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
                                        context.Characters.Remove(characters[i]);
                                        daisysRemoved += 1;
                                        if (daisysRemoved == daisysToRemove) {
                                            break; // so dont remove more than needed
                                        }
                                    }
                                }

                                for (int j = 0; j < eligibleDaisys; j++) {
                                    context.Characters.Add(new Peach { // add transformed character
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
                                        context.Characters.Remove(characters[i]);
                                        wariosRemoved += 1;
                                        if (wariosRemoved == wariosToRemove) {
                                            break;
                                        }
                                    }
                                }

                                for (int j = 0; j < eligibleWarios; j++) {
                                    context.Characters.Add(new Mario {
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
                                        context.Characters.Remove(characters[i]);
                                        waluigisRemoved += 1;
                                        if (waluigisRemoved == waluigisToRemove) {
                                            break;
                                        }
                                    }
                                }

                                for (int j = 0; j < eligibleWaluigis; j++) {
                                    context.Characters.Add(new Luigi {
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
                        context.Dispose();
                    } else {
                        UpdateDB(context);
                        RemoveTempFiles();
                    }
                }
            }

            catch {
                Console.WriteLine("");
                Console.WriteLine("An error occurred while transforming characters. Most likely a database connection issue.");
            }
        }

        static void RemoveCharacter(){ // Additional simple useful feature
            try {
                using (var context = new DatabaseContext()) {
                    context.Database.EnsureCreated();
                    var characters = context.Characters.ToList();

                    if (characters.Count == 0) {
                        Console.WriteLine("");
                        Console.WriteLine("No characters in your pocket to remove!");
                        context.Dispose();
                        RemoveTempFiles();
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
                            RemoveTempFiles();
                            return;
                        }

                        string characterName = characters[convertedCharacterToDelete - 1].Name;

                        context.Characters.Remove(characters[convertedCharacterToDelete - 1]);
                        UpdateDB(context);
                        RemoveTempFiles();
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

        static void BattleAgainstPC() { // Additional comprehensive, creative and useful feature
            using (var context = new DatabaseContext()) {
                context.Database.EnsureCreated();
                var characters = context.Characters.ToList();

                if (characters.Count == 0) {
                    Console.WriteLine("");
                    Console.WriteLine("You don't have any character to battle with!");
                    context.Dispose();
                    RemoveTempFiles();
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
                        string characterName = characters[convertedChosenCharacter - 1].Name;
                        string characterSkill = characters[convertedChosenCharacter - 1].Skill;
                        int characterDMG = characters[convertedChosenCharacter - 1].DMG;
                        bool characterDodgedMove = false;
                        int characterDodgesLeft = 2; // player only has 2 dodges

                        Console.Write("Please choose your opponent: ");
                        string opposingCharacter = Console.ReadLine();
                        Console.WriteLine("-------------------------------------");

                        if (int.TryParse(opposingCharacter, out int convertedOpposingCharacter)) {
                            if ((convertedOpposingCharacter <= characters.Count) && (convertedOpposingCharacter > 0)) {
                                string opposingCharacterName = characters[convertedOpposingCharacter - 1].Name;
                                string opposingCharacterSkill = characters[convertedOpposingCharacter - 1].Skill;
                                int opposingCharacterDMG = characters[convertedOpposingCharacter - 1].DMG;
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
                                    characters[convertedChosenCharacter - 1].EXP += 10;  // gain 10 EXP if u win
                                    UpdateDB(context);
                                    RemoveTempFiles();
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
                                RemoveTempFiles();
                            }
                            else {
                                Console.WriteLine("");
                                Console.WriteLine("No such character!");
                                context.Dispose();
                                RemoveTempFiles();
                                return;
                            }
                        }
                        else {
                            Console.WriteLine("");
                            Console.WriteLine("Please enter a valid integer.");
                            context.Dispose();
                            RemoveTempFiles();
                            return;
                        }
                    }
                    else {
                        Console.WriteLine("");
                        Console.WriteLine("No such character!");
                        context.Dispose();
                        RemoveTempFiles();
                        return;
                    }
                }
                else {
                    Console.WriteLine("");
                    Console.WriteLine("Please enter a valid integer.");
                    context.Dispose();
                    RemoveTempFiles();
                    return;
                }
            }
        }
    }
}
