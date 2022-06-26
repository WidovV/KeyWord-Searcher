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

            do
            {
                if (key == "d")
                {
                    Console.Write("How many directories do you want to search in: ");
                }
                else
                {
                    Console.Write("How many files do you want to search in: ");
                }
                
                tmp = Console.ReadLine();
                //num = Convert.ToInt32(Console.ReadLine());

            } while (!int.TryParse(tmp, out num));



            // Ask which keyword that should be searched for & define kw to their answer
            Console.Write("\nkeyword to search for: ");
            kw = Console.ReadLine();

            // Do-while loop to secure we get one of the answers we expect
            do
            {
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


            if (keyP == "y")
            {
                Console.Write("Destination of the saved file: ");
                pathS = Console.ReadLine();

            }



            ClearCurrentConsoleLine();

            switch (key)
            {
                case "d":

                    // For-loop based on how many files they want to check through
                    for (int f = 0; f < num; f++)
                    {
                        do
                        {
                            Console.Write("\nExample: C:\\skoledata\\\nDefine a path for the directory: ");
                            //Console.Write(" ");
                            path = Console.ReadLine();

                            dir = Directory.GetFiles(path);

                            if (Directory.Exists(path))
                            {
                                for (int i = 0; i < dir.Length; i++)
                                {

                                    path = dir[i];
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
                                                //Console.WriteLine($"Test: {pathS}");
                                                fileS += pathFile[j] + Environment.NewLine;
                                            }

                                            //Console.WriteLine("Case: N");
                                            match++;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nPath not found!\nTry again!\n");
                                num++;
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        } while (!File.Exists(path));
                    }

                    Console.WriteLine($"Keyword: {kw}\nfound in total: {match}");
                    if (keyP == "y")
                    {
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


            do
            {
                Console.WriteLine("\n[S] Search Again\t[C] Close");

                key = Convert.ToString(Console.ReadKey().KeyChar).ToLower();

                if (key == "s")
                {
                    Console.Clear();
                    goto start;
                }
                else if (key == "c")
                {
                    do
                    {
                        ClearCurrentConsoleLine();
                        Console.WriteLine("Are you sure you want to close? [Y/N]");
                        key = Convert.ToString(Console.ReadKey().KeyChar).ToLower();

                        if (key == "y")
                        {
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

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
