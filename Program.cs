using Days;
string[] games = System.IO.File.ReadAllLines(@"./input.txt");

var day = new Day6();

Console.WriteLine($"Part One: {day.SolvePartOne(games)}");
Console.WriteLine($"Part Two: {day.SolvePartTwo(games)}");



