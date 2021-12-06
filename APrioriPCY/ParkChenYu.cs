using System;
using System.Collections.Generic;
using System.Linq;

namespace APrioriPCY
{
    public class ParkChenYu
    {
        public Func<string> ReadLine { get; set; }
        public Action<string> WriteLine { get; set; }
        private int _numberOfBaskets;
        private double _thresholdPercentage;
        private int _hashNumber;
        private int _frequentItems;
        private int _frequentPairs;

        public ParkChenYu(Func<string> readLine, Action<string> writeLine)
        {
            ReadLine = readLine;
            WriteLine = writeLine;
        }

        public void Run()
        {
            Dictionary<int, int> itemCounts = new Dictionary<int, int>();
            List<List<int>> baskets = new List<List<int>>();
            ReadInputData(itemCounts, baskets);

            int[] hashPockets = new int[_hashNumber];
            CompressData(itemCounts, baskets, hashPockets);

            int[,] pairCounter = new int[itemCounts.Count, itemCounts.Count];
            CountFrequentPairs(pairCounter, hashPockets, baskets, itemCounts);

            List<int> sortedCounters = FrequentPairsCounts(pairCounter, hashPockets, itemCounts.Keys.ToList());

            WriteResults(sortedCounters);
        }

        private void WriteResults(List<int> sortedCounters)
        {
            WriteLine((_frequentItems * (_frequentItems - 1) / 2).ToString());
            WriteLine(_frequentPairs.ToString());
            foreach (var counter in sortedCounters)
            {
                WriteLine(counter.ToString());
            }
        }

        private static int GetThreshold(double percentage, int numOfBuckets)
        {
            return (int) Math.Round(percentage * numOfBuckets);
        }

        private List<int> FrequentPairsCounts(int[,] pairCounter, int[] hashPockets, List<int> items)
        {
            items.Sort();
            int threshold = ParkChenYu.GetThreshold(_thresholdPercentage, _numberOfBaskets);
            List<int> values = new List<int>();
            for (int first = 0; first < items.Count - 1; first++)
            {
                for (int second = first + 1; second < items.Count; second++)
                {
                    int pocket = ((items[first] * items.Count) + items[second]) % _hashNumber;
                    if (hashPockets[pocket] >= threshold)
                    {
                        _frequentPairs++;
                        if(pairCounter[items[first], items[second]] >= threshold) 
                            values.Add(pairCounter[items[first], items[second]]);
                    }
                }
            }

            values.Sort((f,s) => -f.CompareTo(s));
            return values;
        }

        private void CountFrequentPairs(int[,] pairCounter, 
            int[] hashPockets, List<List<int>> baskets, Dictionary<int, int> itemCounts)
        {
            int threshold = ParkChenYu.GetThreshold(_thresholdPercentage, _numberOfBaskets);
            int numberOfItems = itemCounts.Count;
            foreach (var basket in baskets)
            {
                for (int first = 0; first < basket.Count-1; first++)
                {
                    for (int second = first + 1; second < basket.Count; second++)
                    {
                        if (itemCounts[basket[first]] >= threshold && itemCounts[basket[second]] >= threshold)
                        {
                            int smaller = Math.Min(basket[first], basket[second]);
                            int greater = Math.Max(basket[first], basket[second]);
                            int pocket = ((smaller * numberOfItems) + greater) % _hashNumber;
                            if (hashPockets[pocket] >= threshold)
                            {
                                pairCounter[smaller, greater]++;
                            }
                        }
                    }
                }
            }
        }

        private void CompressData(Dictionary<int, int> itemCounts, List<List<int>> baskets, int[] hashPockets)
        {
            int threshold = ParkChenYu.GetThreshold(_thresholdPercentage, _numberOfBaskets);
            int numberOfItems = itemCounts.Count;
            foreach (var basket in baskets)
            {
                for (int first = 0; first < basket.Count-1; first++)
                {
                    for (int second = first + 1; second < basket.Count; second++)
                    {
                        if (itemCounts[basket[first]] >= threshold && itemCounts[basket[second]] >= threshold)
                        {
                            int smaller = Math.Min(basket[first], basket[second]);
                            int greater = Math.Max(basket[first], basket[second]);
                            int pocket = ((smaller * numberOfItems) + greater) % _hashNumber;
                            hashPockets[pocket]++;
                        }
                    }
                }
            }
        }

        private void ReadInputData(Dictionary<int, int> itemCounts, List<List<int>> baskets)
        {
            _numberOfBaskets = int.Parse(ReadLine());
            _thresholdPercentage = double.Parse(ReadLine());
            _hashNumber = int.Parse(ReadLine());

            for (int i = 0; i < _numberOfBaskets; i++)
            {
                List<int> basket = ReadLine().Split(null).Select(int.Parse).ToList();
                foreach(int item in basket)
                {
                    if(!itemCounts.ContainsKey(item)) itemCounts.Add(item, 0);
                    itemCounts[item]++;
                }
                baskets.Add(basket);
            }

            int threshold = ParkChenYu.GetThreshold(_thresholdPercentage, _numberOfBaskets);
            _frequentItems = itemCounts.Values.Count(n => n >= threshold);
        }
    }
}
