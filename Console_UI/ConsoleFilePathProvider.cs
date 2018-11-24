using System;
using System.IO;
using ApplicationLogic.Interfaces;

namespace Console_UI
{
    public class ConsoleFilePathProvider : IFilePathProvider
    {
        public string GetFilePath(string extension)
        {
            while (true)
            {
                Console.Write("Provide file path: ");
                string result = Console.ReadLine();

                if (File.Exists(result))
                {
                    if (Path.GetExtension(result) == extension)
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine($"File is not a {extension} file");
                    }
                }
                else
                {
                    Console.WriteLine("File doesn't exist");
                }
            }
        }
    }
}
