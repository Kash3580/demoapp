using System;
using System.IO;
using System.Net;

class Program
{
    static void Main()
    {
        string commandFile = "commands.txt";
        string[] commands = File.ReadAllLines(commandFile);

        foreach (string command in commands)
        {
            string[] commandParts = command.Split(';');
            string commandName = commandParts[0];
            string[] commandParameters = commandParts[1].Split(',');

            ExecuteCommand(commandName, commandParameters);
        }
    }

    static void ExecuteCommand(string commandName, string[] commandParameters)
    {
        switch (commandName)
        {
            case "File Copy":
                string sourceFile = commandParameters[0];
                string destinationFile = commandParameters[1];
                File.Copy(sourceFile, destinationFile);
                Console.WriteLine($"File copied from {sourceFile} to {destinationFile}");
                break;

            case "File Delete":
                string filePath = commandParameters[0];
                File.Delete(filePath);
                Console.WriteLine($"File deleted: {filePath}");
                break;

            case "Query Folder Files":
                string folderPath = commandParameters[0];
                string[] files = Directory.GetFiles(folderPath);
                Console.WriteLine("Files in folder:");
                foreach (string file in files)
                {
                    Console.WriteLine(file);
                }
                break;

            case "Create Folder":
                string folderPath = commandParameters[0];
                string newFolderName = commandParameters[1];
                Directory.CreateDirectory(Path.Combine(folderPath, newFolderName));
                Console.WriteLine($"Folder created: {Path.Combine(folderPath, newFolderName)}");
                break;

            case "Download File":
                string sourceUrl = commandParameters[0];
                string outputFile = commandParameters[1];
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(sourceUrl, outputFile);
                }
                Console.WriteLine($"File downloaded from {sourceUrl} to {outputFile}");
                break;

            case "Wait":
                int waitTime = int.Parse(commandParameters[0]);
                System.Threading.Thread.Sleep(waitTime * 1000);
                Console.WriteLine($"Waited for {waitTime} seconds");
                break;

            case "Conditional Count Rows File":
                string sourceFile = commandParameters[0];
                string searchString = commandParameters[1];
                int rowCount = 0;
                using (StreamReader reader = new StreamReader(sourceFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(searchString))
                        {
                            rowCount++;
                        }
                    }
                }
                Console.WriteLine($"Count of rows containing '{searchString}': {rowCount}");
                break;

            default:
                Console.WriteLine($"Unknown command: {commandName}");
                break;
        }
    }
}
