using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Link: ");
        string link = Console.ReadLine();

        Console.WriteLine("Audio [1]");
        Console.WriteLine("Video [2]");
        Console.Write("#> ");
        string choice = Console.ReadLine();

        Process process = new Process();
        process.StartInfo.FileName = "yt-dlp" + (choice == "2" ? ".exe" : ""); // Add ".exe" if choice is video
        process.StartInfo.Arguments = choice == "1" ? $"--extract-audio --audio-format mp3 {link}" : $"-S \"ext\" {link}";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;

        process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
        process.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data);

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();

        // Keep the console window open until a key is pressed
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}