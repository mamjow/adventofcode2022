using Days;
string[] games = System.IO.File.ReadAllLines(@"./input.txt");

var day = new Day8();

Console.WriteLine($"Part One: {day.SolvePartOne(games)}");
Console.WriteLine($"Part Two: {day.SolvePartTwo(games)}");



