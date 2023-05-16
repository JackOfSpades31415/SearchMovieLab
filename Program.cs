﻿using NLog;
string path = Directory.GetCurrentDirectory() + "\\nlog.config";
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Start!");

Console.WriteLine("Enter 1 to add movie.");
Console.WriteLine("Enter 2 to list movies.");
Console.WriteLine("Enter 3 to Serach movies by name.");
Console.WriteLine("Enter anything else to quit.");

String prompt = Console.ReadLine();
if (prompt == "1"){

    StreamWriter sw = new StreamWriter("movies.csv", true);
    Console.WriteLine("movie ID?");
    int movieID = 0;
    if (!int.TryParse(Console.ReadLine(), out movieID)){
        throw new Exception("Input Invalid.");
    }
    Console.WriteLine("Name of movie?");
    String movieName = Console.ReadLine();
    bool genreAsk = true;
    List<String> genres = new List<String>();
    char moreGenre;
    while(genreAsk){
        Console.WriteLine("What genre?");
        genres.Add(Console.ReadLine());
        Console.WriteLine("Another Genre? Y/N");
        if (!Char.TryParse(Console.ReadLine(), out moreGenre)){
        throw new Exception("Input Invalid.");
    }
            if(moreGenre == 'n'){
                genreAsk = false;
            }
            else if(moreGenre != 'y' || moreGenre != 'n'){
                logger.Error("Invalid input.");
            }
    }
    Console.WriteLine("Whose the Director?");
    String movieDirector = Console.ReadLine();
    Console.WriteLine("Enter the runtime (h:m:s)");
    String movieTime = Console.ReadLine();
    sw.WriteLine($"{movieID},{movieName},{movieDirector},{movieTime},{string.Join("|", genres)}"); 
    sw.Close();
}
else if(prompt == "2"){
         if(System.IO.File.Exists("movies.csv")){
        StreamReader sr = new StreamReader("movies.csv");
        while(!sr.EndOfStream){
            Console.WriteLine(sr.ReadLine());
        }
         }

}

else if(prompt == "3"){
    Console.WriteLine("Input title keywords you'd like to search by:");
    String searchInput = Console.ReadLine();
    var Movies = MovieFile.Movies.Where(m => m.title.Contains(searchInput));
    int num = MovieFile.Movies.Where(m => m.title.Contains(searchInput)).Count();
    foreach(Movie m in Movies)
{
    Console.WriteLine($"  {m.title}");
}
    Console.WriteLine($"There are {Movies.Count()} movies that fit that search query.");
    
}

string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
logger.Info(scrubbedFile);
MovieFile movieFile = new MovieFile(scrubbedFile);

Console.ForegroundColor = ConsoleColor.Green;

/*
// LINQ - Where filter operator & Contains quantifier operator
var Movies = movieFile.Movies.Where(m => m.title.Contains("(1990)"));
// LINQ - Count aggregation method
Console.WriteLine($"There are {Movies.Count()} movies from 1990");

// LINQ - Any quantifier operator & Contains quantifier operator
var validate = movieFile.Movies.Any(m => m.title.Contains("(1921)"));
Console.WriteLine($"Any movies from 1921? {validate}");

int num = movieFile.Movies.Where(m => m.title.Contains("(1921)")).Count();
Console.WriteLine($"There are {num} movies from 1921");

// LINQ - Where filter operator & Contains quantifier operator
var Movies1921 = movieFile.Movies.Where(m => m.title.Contains("(1921)"));
foreach(Movie m in Movies1921)
{
    Console.WriteLine($"  {m.title}");
}

// LINQ - Where filter operator & Select projection operator & Contains quantifier operator
var titles = movieFile.Movies.Where(m => m.title.Contains("Shark")).Select(m => m.title);
// LINQ - Count aggregation method
Console.WriteLine($"There are {titles.Count()} movies with \"Shark\" in the title:");
foreach(string t in titles)
{
    Console.WriteLine($"  {t}");
}

// LINQ - First element operator
var FirstMovie = movieFile.Movies.First(m => m.title.StartsWith("Z", StringComparison.OrdinalIgnoreCase));
Console.WriteLine($"First movie that starts with letter 'Z': {FirstMovie.title}");


Console.ForegroundColor = ConsoleColor.White;
*/




logger.Info("End!");
