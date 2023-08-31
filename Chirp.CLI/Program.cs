using System.ComponentModel;
using System.ComponentModel.Design;
using Microsoft.Win32.SafeHandles;

if (args[0] == "read")
{
    // The following is adapted from https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-read-text-from-a-file
    try
    {
        var reader = new StreamReader("data/chirp_cli_db.csv");
        var cheeps = new List<string>();
        reader.ReadLine();
        while (reader.Peek() >= 0)
        {
            string line = reader.ReadLine();
            string[] words = line.Split(',');
            string author = words[0];
            string message = words[1];
            int timestamp = int.Parse(words[2]);
            var time = DateTimeOffset.FromUnixTimeSeconds(timestamp);
            string output = $"{author} @ {time}: {message}";
            cheeps.Add(output);
        }
        foreach (string cheep in cheeps)
        {
            Console.WriteLine(cheep);
        }
    }
    catch (IOException e)
    {
        Console.WriteLine("The file could not be read");
        Console.WriteLine(e.Message);
    }
}