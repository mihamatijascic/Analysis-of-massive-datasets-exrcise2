using System;
using System.Diagnostics;
using System.IO;

namespace APrioriPCY
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //D:\faks\8.semestar\Analiza_velikih_skupova_podataka\Labosi\Labos2\APrioriPCY\APrioriPCY\InputData\R.in
            StreamReader read = new StreamReader("InputData/R.in");
            StreamWriter write = new StreamWriter("InputData/result.out");

            Stopwatch st = Stopwatch.StartNew();
            ParkChenYu algorithm = new ParkChenYu(read.ReadLine, Console.WriteLine);
            algorithm.Run();
            Console.WriteLine(st.Elapsed);
            read.Close();
            write.Close();
        }
    }
}
