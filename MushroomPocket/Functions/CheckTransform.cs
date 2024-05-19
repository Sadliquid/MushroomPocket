using System;
using System.Collections.Generic;
using System.Linq;

namespace MushroomPocket.Functions {
    public static class CheckTransformFunction {
        static bool noCharactersToTransform = true;
        public static void CheckTransform(List<MushroomMaster> mushroomMasters) {
            noCharactersToTransform = true;
            List<string> eligibleTransformations = new List<string>();
            using (var context = new DatabaseContext()) {
                context.Database.EnsureCreated();
                var characters = context.Characters.ToList();

                if (characters.Count == 0) {
                    Console.WriteLine("");
                    Console.WriteLine("No characters in your pocket.");
                    context.Dispose();
                    DatabaseManagementFunctions.RemoveTempFiles();
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
                DatabaseManagementFunctions.RemoveTempFiles();
            }
        }
    }
}