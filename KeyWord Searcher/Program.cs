using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;

namespace KeyWord_Searcher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set the window-title matching the usage of the program
            Console.Title = "Keyword Searcher";

            start:
            string path, pathS = "", tmp, kw, key, keyC, keyP, fileS = "";
            string[] dir, pathFile;
            int num;
            int match = 0;
            double version = 0.1;

            // Defualt information
            Console.WriteLine("This program is made for searching keyword(s) in one- or multiple files.");
            Console.WriteLine($"Version: {version}\n");

            Console.WriteLine("[D] Directories\t[F] Files");

            key = Convert.ToString(Console.ReadKey().KeyChar).ToLower();
            ClearCurrentConsoleLine();

            // Do-while loop to secure their input is as we expect
            do
            {
                // if statements to differ the word of choice
                if (key == "d")
                {
                    Console.Write("How many directories do you want to search in: ");
                }
                else
                {
                    Console.Write("How many files do you want to search in: ");
                }
                // Save to a temporary string to save it into a int
                tmp = Console.ReadLine();
                //num = Convert.ToInt32(Console.ReadLine());

            } while (!int.TryParse(tmp, out num));



            // Ask which keyword that should be searched for & define kw to their answer
            Console.Write("\nkeyword to search for: ");
            kw = Console.ReadLine();

            // Do-while loop to secure we get one of the answers we expect
            do
            {
                // Asking the relevant question, getting their input to our variable and clear the current line to not see a unexpected "y" or "n" on the screen
                Console.WriteLine("Should upper- or lowercase matter? [Y/N]");
                keyC = Convert.ToString(Console.ReadKey().KeyChar).ToLower();
                ClearCurrentConsoleLine();
            } while (keyC != "y" && keyC != "n");

            do
            {
                Console.WriteLine("Should the lines including the keyword be printed to a file? [Y/N]");
                keyP = Convert.ToString(Console.ReadKey().KeyChar).ToLower();
                ClearCurrentConsoleLine();
            } while (keyP != "y" && keyP != "n");

            // If their answer was "y" ask which destination it should be saved in
            if (keyP == "y")
            {
                Console.Write("Destination of the saved file: ");
                // Save their choice to our variable
                pathS = Console.ReadLine();

            }
            
            // Clear 
            ClearCurrentConsoleLine();

            // Switch cases with their pressed key from a question further up
            switch (key)
            {
                case "d":

                    // For-loop based on how many files they want to check through
                    for (int f = 0; f < num; f++)
                    {
                        // Ask the question again if their input is unreachable/not created
                        do
                        {
                            // Show an example of usage and ask for the directory
                            Console.Write("\nExample: C:\\skoledata\\\nDefine a path for the directory: ");

                            path = Console.ReadLine();
                            dir = Directory.GetFiles(path);

                            // If statement to secure the directory is there
                            if (Directory.Exists(path))
                            {
                                // For-loop based on how many files there is in the directory
                                for (int i = 0; i < dir.Length; i++)
                                {
                                    // Save the filename to path
                                    path = dir[i];
                                    // Read the file from the path and save it in pathFile
                                    pathFile = File.ReadAllLines(path);

                                    // For loop based on the length of the file
                                    for (int j = 0; j < pathFile.Length; j++)
                                    {
                                        // If case to check their case-choice
                                        if (keyC == "n")
                                        {
                                            // Read everything as lowercase
                                            pathFile[j] = pathFile[j].ToLower();
                                        }
                                        
                                        // Check if the line contains the keyword
                                        if (pathFile[j].Contains(kw))
                                        {
                                            // If their choice of saving is true
                                            if (keyP == "y")
                                            {
                                                //Save the line to a file including a new line afterwards
                                                fileS += pathFile[j] + Environment.NewLine;
                                            }

                                            //Console.WriteLine("Case: N");
                                            // Counter for number of matches
                                            match++;
                                        }
                                    }
                                }
                            }
                            // If the directory doesn't exist
                            else
                            {
                                // Set the color to red and let them know we didnt find the path
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nPath not found!\nTry again!\n");
                                // To secure they get the chosen amount of question even tho one of the directories wasnt reachable, plus with one
                                num++;
                                // Reset the color white
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        } while (!Directory.Exists(path));
                    }
                    // Inform of the keyword we searched for and how many were found in total
                    Console.WriteLine($"Keyword: {kw}\nfound in total: {match}");
                    
                    // If they wanted to save it to a file
                    if (keyP == "y")
                    {
                        // Write the text to a file
                        File.WriteAllText(pathS, fileS);
                    }
                    
                    break;

                case "f":

                    // For-loop based on how many files they want to check through
                    for (int i = 0; i < num; i++)
                    {
                        Console.Write("Example: C:\\skoledata\\file.txt \nDefine a path for the file: ");
                        //Console.Write(" ");
                        path = Console.ReadLine();
                        if (File.Exists(path))
                        {
                            pathFile = File.ReadAllLines(path);

                            for (int j = 0; j < pathFile.Length; j++)
                            {
                                if (keyC == "n")
                                {
                                    pathFile[j] = pathFile[j].ToLower();
                                }

                                if (pathFile[j].Contains(kw))
                                {
                                    if (keyP == "y")
                                    {
                                        fileS += pathFile[j] + Environment.NewLine;
                                    }
                                    
                                    match++;
                                }
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nFile not found!\nTry again!\n");

                            Console.ForegroundColor = ConsoleColor.White;
                            num++;
                        }
                    }

                    Console.WriteLine($"Keyword: {kw}\nfound in total: {match}");

                    if (keyP == "y")
                    {
                        File.WriteAllText(pathS, fileS);
                    }

                    break;
            }

            // Do-while loop to secure their keypress
            do
            {
                // Inform which options they have from here on
                Console.WriteLine("\n[S] Search Again\t[C] Close");

                key = Convert.ToString(Console.ReadKey().KeyChar).ToLower();
                // If they want to search again, clear the console and go to start
                if (key == "s")
                {
                    Console.Clear();
                    goto start;
                }
                else if (key == "c")
                {
                    do
                    {
                        // Make sure they are sure they want to close
                        ClearCurrentConsoleLine();
                        Console.WriteLine("Are you sure you want to close? [Y/N]");
                        key = Convert.ToString(Console.ReadKey().KeyChar).ToLower();

                        if (key == "y")
                        {
                            // If they are sure, close the program
                            Environment.Exit(0);
                        }
                        else if (key == "n")
                        {
                            goto start;
                        }
                    } while (!key.Contains("yn"));
                }
            } while (!key.Contains("sc"));
            
            Console.ReadKey();
        }

        // Method found on stackoverflow, thanks
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
