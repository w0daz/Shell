using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var shell = new Shell();
            shell.Run();
        }
    }

    public class Shell
    {
        public Shell() { 
            startDir = Directory.GetCurrentDirectory();
        }
        private string startDir { get; set; }
        public string CommandArguments { get;  set; }

        private Dictionary<string, string> Aliases = new Dictionary<string, string>
        {
            { "ls", @".\ListDirectories.exe" },
            { "clear", @".\Clear.exe" },
            { "wc", @".\WordCount.exe"}
        };

        public void Run()
        {
            string input = null;

            do
            {
                Console.Write("$ ");
                input = Console.ReadLine();
                Execute(input);
            } while (input != "exit");
        }

        public int Execute(string input)
        {
            string[] splitInput = input.Split(' ');

            if (Aliases.Keys.Contains(splitInput[0]))
            {
                string commandArguments = "";
                for (int i = 0; i < splitInput.Length; i++)
                {
                    commandArguments = commandArguments + splitInput[i] + " ";
                }

                var process = new Process();
                Console.WriteLine($@"{startDir}\{Aliases[splitInput[0]]}");
                process.StartInfo = new ProcessStartInfo($@"{startDir}\{Aliases[splitInput[0]]}")
                {
                    UseShellExecute = false,
                    Arguments = commandArguments
                };

                process.Start();
                process.WaitForExit();

                return 0;
            }

            // Check for the pwd command
            if (splitInput[0].ToLower() == "pwd")
            {
                PrintCurrentDirectory();
                return 0;
            }

            if (splitInput[0].ToLower().StartsWith("cd"))
            {
                ChangeDirectory(input);
                return 0;
            }

            //check if input is to read oliver_twist file and count words
           /* if (input.ToLower().StartsWith("wc "))
            {
                string filePath = Path.Combine(startDir, input.Substring(3).Trim());

                if (File.Exists(filePath))
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(filePath))
                        {
                            string content = sr.ReadToEnd();
                            int wordCount = content.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
                            Console.WriteLine($"Word count of {filePath}: {wordCount}");
                        }
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading file: {ex.Message}");
                        return 1;
                    }
                }

            }*/

            Console.WriteLine($"{input} not found");
            return 1;
        }
        private void PrintCurrentDirectory()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
        }

        private void ChangeDirectory(string input)
        {
            string path = input.Substring(2).Trim();

            try
            {
                Directory.SetCurrentDirectory(path);
                Console.WriteLine($"Changed directory to: {Directory.GetCurrentDirectory()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error changing directory: {ex.Message}");
            }

        }


    }
}
