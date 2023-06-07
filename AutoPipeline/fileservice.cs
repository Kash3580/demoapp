using System;
using System.IO;
using System.Net;
using System.Threading;

class Program
{
    static void Main()
    {
        ExecuteCommands();
    }

    static void ExecuteCommands()
    {
        Console.WriteLine("Enter commands  or 'exit' to quit:");
        Console.WriteLine("1. copy:<sourcefile> <destinationfile>");
        Console.WriteLine("2. delete:<sourcefile>");
        Console.WriteLine("3. files:<folderpath>");
        Console.WriteLine("4. createfolder:<sourcefolderpath> <newfoldername>");
        Console.WriteLine("5. downloadfile:<sourcefile> <destinationfile>");
        Console.WriteLine("6. wait:<seconds>");
        Console.WriteLine("7. waitforhour:<seconds>");
        Console.WriteLine("8. waitforday:<seconds>");

        while (true)
        {
            string command = Console.ReadLine();

            if (command.ToLower() == "exit")
                break;

            string[] commandParts = command.Split(':');
            string commandName = commandParts[0];
            string[] commandParameters = commandParts.Length > 1 ? commandParts[1].Split(' ') : new string[0];

            ExecuteCommand(commandName, commandParameters);
        }
    }

    static void ExecuteCommand(string commandName, string[] commandParameters)
    {
        string folderPath = "";
        switch (commandName)
        {
            case "copy":
                string sourceFile = commandParameters[0];
                string destinationFile = commandParameters[1];
                File.Copy(sourceFile, destinationFile);
                Console.WriteLine($"File copied from {sourceFile} to {destinationFile}");
                break;

            case "delete":
                string filePath = commandParameters[0];
                File.Delete(filePath);
                Console.WriteLine($"File deleted: {filePath}");
                break;

            case "files":
                folderPath = commandParameters[0];
                string[] files = Directory.GetFiles(folderPath);
                Console.WriteLine("Files in folder:");
                foreach (string file in files)
                {
                    Console.WriteLine(file);
                }
                break;

            case "createfolder":
                folderPath = commandParameters[0];
                string newFolderName = commandParameters[1];
                Directory.CreateDirectory(Path.Combine(folderPath, newFolderName));
                Console.WriteLine($"Folder created: {Path.Combine(folderPath, newFolderName)}");
                break;

            case "downloadfile":
                string sourceUrl = commandParameters[0];
                string outputFile = commandParameters[1];
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(sourceUrl, outputFile);
                }
                Console.WriteLine($"File downloaded from {sourceUrl} to {outputFile}");
                break;

            case "wait":
                int waitTime = int.Parse(commandParameters[0]);
                Thread.Sleep(waitTime * 1000);
                Console.WriteLine($"Waited for {waitTime} seconds");
                break;

            case "waitforhour":
                int targetHour = int.Parse(commandParameters[0]);
                DateTime now = DateTime.Now;
                DateTime targetTime = new DateTime(now.Year, now.Month, now.Day, targetHour, 0, 0);
                if (now > targetTime)
                {
                    targetTime = targetTime.AddDays(1);
                }
                TimeSpan waitDuration = targetTime - now;
                Thread.Sleep(waitDuration);
                Console.WriteLine($"Waited until {targetTime.ToString("HH:mm")} for the specified hour");
                break;

            case "waitforday":
                DayOfWeek targetDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), commandParameters[0]);
                now = DateTime.Now;
                targetTime = now.AddDays((7 + (targetDay - now.DayOfWeek)) % 7);
                targetTime = new DateTime(targetTime.Year, targetTime.Month, targetTime.Day, 0, 0, 0);
                TimeSpan waitDurationDay = targetTime - now;
                Thread.Sleep(waitDurationDay);
                Console.WriteLine($"Waited until {targetDay} for the specified day");
                break;

            default:
                Console.WriteLine("Invalid Command, Please try again");
                break;
        }
    }
}
