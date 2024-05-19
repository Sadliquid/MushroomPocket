# MushroomPocket
This is a C# school project that im working on, which covers OOP and Entity Framework

This console application let Super Mario’s players keep, view the characters they played and check if they can transform to a higher role character to protect the Mushroom Kingdom. If the characters in the pocket are ready to transform, the player can transform their characters. Each time the player played a character, they can add the character details to their Mushroom Pocket.

## How to use MushroomPocket
(1). Add a character to your pocket

(2). List character(s) in your pocket

(3). Check if you can transform your characters

(4). Transform all your eligible characters

(5). Remove a character from your pocket

(6). Battle against the PC!

## Notes
- You can only add the 3 base characters (Daisy, Wario, and Waluigi) to your pocket, which can be transformed if you meet the requirements
- You need 2 Daisy's to transform into Peach, 3 Warios to transform into Mario and only 1 Waluigi to transform into Luigi
- All characters' health will be temporarily set to 100 for battle. This won't affect the original health of your characters.
- You can use transformations in Battle Mode
- You only have 2 dodges against the opposing character's attack
- The character that remains with the most HP after all 3 battle rounds wins the battle!
- If your character and the opposing character has the same HP after all 3 battle rounds, the battle will be a draw
- Every battle win gains your playing character +10 EXP (EXP will still be reset to 0 after transforming)

## Character moves
Daisy (Leadership): 20 DMG

Wario (Strength): 25 DMG

Waluigi (Agility): 18 DMG

Peach (Magic Abilities): 22 DMG

Mario (Combat Skills): 30 DMG

Luigi (Precision and Accuracy): 21 DMG

*Mario has the most damage, being the main character in the game!*

## Installed packages
`dotnet add package Microsoft.EntityFrameworkCore.Sqlite`

`dotnet add package Microsoft.EntityFrameworkCore.Design`


## Sources and credits
The following are where I sourced for info and got help from throughout this whole project, as well as owe credit to:
- [Looping though an array (W3 Schools)](https://www.w3schools.com/cs/cs_arrays_loop.php)
- [Flooring in C# (GeeksForGeeks)](https://www.geeksforgeeks.org/c-sharp-math-floor-method/)
- [C# Operators (W3 Schools)](https://www.w3schools.com/cs/cs_operators.php)
- [Bubble Sorting Algorithms (W3 Schools)](https://www.w3schools.com/dsa/dsa_algo_bubblesort.php)
- [C# Classes (W3 Schools)](https://www.w3schools.com/cs/cs_classes.php)
- [C# Class Types (C# Corner)](https://www.c-sharpcorner.com/UploadFile/0c1bb2/types-of-classes-in-C-Sharp1/)
- [Entity Framework Core using SQLite (C# Corner)](https://www.c-sharpcorner.com/article/get-started-with-entity-framework-core-using-sqlite/)
- [Database Contexts in C# (C# Corner)](https://www.google.com/url?sa=t&source=web&rct=j&opi=89978449&url=https://www.c-sharpcorner.com/article/entity-framework-dbcontext/%23:~:text%3DAs%2520per%2520Microsoft%2520%25E2%2580%259CA%2520DbContext,between%2520Entity%2520Framework%2520and%2520Database.&ved=2ahUKEwi8oOjs0peGAxV5e2wGHfwyCAkQFnoECA4QAw&usg=AOvVaw2Ra8ic4MBwiJbUtWeAk2vi)
- [Executing raw SQL statements for SQLite DB operations (Stack Overflow)](https://stackoverflow.com/questions/64125596/how-to-use-context-database-executesqlinterpolated)
- [Manually performing checkpoints in SQLite Databases](https://stackoverflow.com/questions/64125596/how-to-use-context-database-executesqlinterpolated)
- [Getters + Setters in C# (W3 Schools)](https://www.w3schools.com/cs/cs_properties.php)
- [Lists in C# (GeeksForGeeks)](https://www.geeksforgeeks.org/c-sharp-list-class/)
- [Exceptions in C# (W3 Schools)](https://www.w3schools.com/cs/cs_exceptions.php)
- [DbSet (Microsoft)](https://www.google.com/url?sa=t&source=web&rct=j&opi=89978449&url=https://learn.microsoft.com/en-us/dotnet/api/system.data.entity.dbset-1%3Fview%3Dentity-framework-6.2.0%23:~:text%3DA%2520DbSet%2520represents%2520the%2520collection,a%2520DbContext%2520using%2520the%2520DbContext.&ved=2ahUKEwjnp4zyuoiGAxX-d2wGHWiZBbYQFnoECBMQAw&usg=AOvVaw0CYGl-mhiemJrwTPhoZZ8T)

The following AI tools were used to aid me in the Entity Framework section of this assignment:

- [ChatGPT](chatgpt.com)
- [Google Gemini](https://gemini.google.com/app)

- [Link to ChatGPT conversation](https://chat.openai.com/share/7b183fae-63ca-4dd4-9f1d-bbffa0732616)
- [Link to Google Gemini (was previously Bard) conversation](https://g.co/gemini/share/2c1bb1c6831a)


## Submission details

Advanced Programming

Joshua Long (230627W)

IT2154 Group 1
