using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab3
{
    public static class ArithmeticCoding
    {
        private class Interval
        {
            public double Start { get; set; }
            public double End { get; set; }
        }


        public static void Compress(string input_file, string output_file)
        {
            using (StreamReader input = new StreamReader(input_file))
            using (BinaryWriter output = new BinaryWriter(new FileStream(output_file, FileMode.Create),Encoding.UTF8))
            {
                int buffer_size = 32;
                char[] buffer = new char[buffer_size];
                while (!input.EndOfStream)
                {
                    int bytesRead = input.Read(buffer, 0, buffer_size);
                    string data = new string(buffer, 0, bytesRead);
                    data += "\u0092";
                    double output_data = Encode(data);
                    output.Write(output_data);
                }
            }
        }

        private static double Encode(string input)
        {
            //Calculate frequincies
            Dictionary<char, int> frequencies = new Dictionary<char, int>();
            foreach(char c in input)
            {
                if(!frequencies.ContainsKey(c))
                {
                    frequencies.Add(c, 0);
                }
                frequencies[c]++;
            }

            //Calculate intervals
            Dictionary<char, Interval> intervals = new Dictionary<char, Interval>();
            double low = 0.0;
            double high = 1.0;
            foreach(KeyValuePair<char, int> symbol in frequencies)
            {
                char c = symbol.Key;
                int frequency = symbol.Value;
                double range = high - low;
                double charLow = low;
                double charHigh = low + (range * (double)frequency / (double)input.Length);
                intervals.Add(c, new Interval() { Start = charLow, End = charHigh});
                low = charHigh;
            }

            //Encode
            double inputLow = 0.0;
            double inputHigh = 1.0;
            foreach(char c in input)
            {
                double range = inputHigh - inputLow;
                Interval c_interval = intervals[c];
                inputLow = inputLow + (range * intervals[c].Start);
                inputHigh = inputLow + (range * intervals[c].End);
            }

            //Getting string to write in file
            double result = inputLow + (inputHigh - inputLow) / 2.0;
            return result;
        }
    }
}
