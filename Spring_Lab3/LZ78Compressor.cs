using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab3
{
    public static class LZ78Compressor
    {
        public static void Compress(string input_file, string output_file)
        {
            using (StreamReader input = new StreamReader(input_file))
            using (StreamWriter output = new StreamWriter(output_file))
            {
                Encode(input, output);
            }
        }

        private static void Encode(StreamReader input, StreamWriter output)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            int index = 0;

            while (!input.EndOfStream)
            {
                string curr = $"{Convert.ToChar(input.Read())}";

                if(dict.ContainsKey(curr))
                {
                    index = dict[curr];
                } else
                {
                    dict.Add(curr, dict.Count + 1);
                    output.Write($"({index},{curr})");
                    index = 0;
                }
            }

            if(index > 0)
            {
                output.Write($"({index},)");
            }
        }
    }
}
