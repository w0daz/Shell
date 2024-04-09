using System;
using System.IO;

namespace WordCount
{
    class Program
    {
        static int Main(string[] args)
        {

            try
            {
                foreach (string arg in args) { Console.WriteLine(arg); }


                if (args[0].ToLower().StartsWith("wc"))
                {
                    //string filePath = Directory.GetCurrentDirectory() + "\\" + args[1];
                    string filePath = args[1];


                    //Console.WriteLine(filePath);

                    if (File.Exists(filePath))
                    {
                        try
                        {
                            using (StreamReader sr = new StreamReader(filePath))
                            {
                                string content = sr.ReadToEnd();
                                int wordCount = content.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
                                Console.WriteLine($"Word count: {wordCount}");
                            }
                            return 0;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error reading file: {ex.Message}");
                            return 1;
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Invalid command. Please us 'wc' ");
                }
                Console.WriteLine("File not found.");
                return 1;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("No command-line arguments provided.");
                return 1; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
                return 1;
            }
        }
    }
}