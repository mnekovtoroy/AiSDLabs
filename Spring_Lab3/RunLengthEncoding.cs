using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab3
{
    public static class RunLengthEncoding
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
            string separator = "\u0091";
            if (input.EndOfStream) return;
            char last_c = Convert.ToChar(input.Read());
            int counter = 1;
            while(!input.EndOfStream)
            {
                char curr = Convert.ToChar(input.Read());
                if(curr != last_c)
                {
                    output.Write($"{counter}{separator}{last_c}");
                    last_c = curr;
                    counter = 1;
                } else
                {
                    counter++;
                }
            }
            output.Write($"{counter}{separator}{last_c}");
        }
    }
}
