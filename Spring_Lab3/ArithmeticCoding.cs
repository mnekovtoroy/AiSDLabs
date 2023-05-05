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
            using (BinaryReader input = new BinaryReader(new FileStream(input_file, FileMode.Open)))
            using (BinaryWriter output = new BinaryWriter(new FileStream(output_file, FileMode.Create), Encoding.UTF8))
            {
                int buffer_size = 11;
                Byte[] buffer = new Byte[buffer_size];
                while (input.BaseStream.Position < input.BaseStream.Length)
                {
                    int bytesRead = input.Read(buffer, 0, buffer_size);
                    Byte[] data = new Byte[bytesRead + 1];
                    Array.Copy(buffer, data, bytesRead);
                    data[data.Length - 1] = 0;
                    //data += "\u0092";
                    double output_data = Encode(data);
                    output.Write(output_data);
                }
            }
        }

        private static double Encode(Byte[] input)
        {
            //Calculate frequincies
            Dictionary<Byte, int> frequencies = new Dictionary<Byte, int>();
            foreach(Byte c in input)
            {
                if(!frequencies.ContainsKey(c))
                {
                    frequencies.Add(c, 0);
                }
                frequencies[c]++;
            }

            //Calculate intervals
            Dictionary<Byte, Interval> intervals = new Dictionary<Byte, Interval>();
            double low = 0.0;
            double high = 1.0;
            foreach(KeyValuePair<Byte, int> symbol in frequencies)
            {
                Byte c = symbol.Key;
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
            foreach(Byte c in input)
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
