using System;
using System.Diagnostics;
using System.IO;

namespace APrioriPCY
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StreamReader read = new StreamReader("InputData/R.in");
            StreamWriter write = new StreamWriter("InputData/result.out");

            ParkChenYu algorithm = new ParkChenYu(read.ReadLine, write.WriteLine);
            algorithm.Run();
            read.Close();
            write.Close();

            var solutionLines = File.ReadAllLines("InputData/R.out");
            var resultLines = File.ReadAllLines("InputData/result.out");

            int correctCounter = 0;
            for(int i = 0; i < solutionLines.Length; i++)
            {
                if (solutionLines[i] == resultLines[i]) correctCounter++;
            }

            Console.WriteLine($"accuracy; {correctCounter}/{solutionLines.Length}");
        }
    }
}
