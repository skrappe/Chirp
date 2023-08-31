using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using Microsoft.Win32.SafeHandles;

var filename = "data/chirp_cli_db.csv";

if (args[0] == "read")
{
    // The following is adapted from https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-read-text-from-a-file
    try
    {
        var reader = new StreamReader(filename);
        var cheeps = new List<string>();
        reader.ReadLine();
        while (reader.Peek() >= 0)
        {
            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            string line = reader.ReadLine();
            string[] words = CSVParser.Split(line);
            string author = words[0];
            string message = words[1];
            int timestamp = int.Parse(words[2]);
            var time = DateTimeOffset.FromUnixTimeSeconds(timestamp).ToLocalTime();
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
else if (args[0] == "cheep")
{
    try
    {
        string user = "kaaj";
        var time = DateTime.Now;
        long unixTime = ((DateTimeOffset)time).ToUnixTimeSeconds();
        string input = $"{user},\"{args[1]}\",{unixTime}";
        File.AppendAllText(filename, Environment.NewLine + input);
    }
    catch (IOException e)
    {
        Console.WriteLine("The file does not exist");
        Console.WriteLine(e.Message);
    }
}