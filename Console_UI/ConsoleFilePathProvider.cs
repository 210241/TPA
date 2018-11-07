using System;
using System.IO;
using ApplicationLogic.Interfaces;

namespace Console_UI
{
    public class ConsoleFilePathProvider : IFilePathProvider
    {
        public string GetFilePath()
        {
            string result;

            while (true)
            {
                Console.Write("Provide file path: ");
                result = Console.ReadLine();

                if (File.Exists(result))
                {
                    if (Path.GetExtension(result) == ".dll")
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("File is not a .dll assembly");
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
