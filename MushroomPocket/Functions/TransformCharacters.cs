using System;
using System.Collections.Generic;
using System.Linq;

namespace MushroomPocket.Functions {
    public static class TransformCharactersFunction { 
        static bool noEligibleTransformations = true;
        public static void TransformCharacters(List<MushroomMaster> mushroomMasters) {
            noEligibleTransformations = true;
            try {
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
                        DatabaseManagementFunctions.UpdateDB(context);
                        DatabaseManagementFunctions.RemoveTempFiles();
                    }
                }
            }

            catch {
                Console.WriteLine("");
                Console.WriteLine("An error occurred while transforming characters. Most likely a database connection issue.");
            }
        }
    }
}