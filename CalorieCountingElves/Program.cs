using CalorieCountingElves.Project;
using System.Runtime.InteropServices;

var values = "C X\r\nC X\r\nC X\r\nA Z\r\nC X\r\nC X\r\nA Y\r\nB X\r\nB Y\r\nB Z";

Console.WriteLine($"Points Won: {RockPaperScissorsProcessor.GetScore(new RockPaperScissorsProcessor { InputString = values })}");
