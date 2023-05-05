using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab3
{
    public static class MTFCompressor
    {
        private static List<char> Alphabet { get; set; }

        public static void Compress(string input_file, string output_file)
        {
            using (StreamReader input = new StreamReader(new FileStream(input_file, FileMode.Open), Encoding.UTF8))
            {
                InitializeAlphabet(input);
            }
            using (StreamReader input = new StreamReader(new FileStream(input_file, FileMode.Open), Encoding.UTF8))
            using (StreamWriter output = new StreamWriter(new FileStream(output_file, FileMode.Create), Encoding.UTF8))
            {
                Encode(input, output);
            }
        }

        private static void InitializeAlphabet(StreamReader input)
        {
            Alphabet = new List<char>();
            var added = new HashSet<char>();
            while (input.BaseStream.Position != input.BaseStream.Length)
            {
                char c = Convert.ToChar(input.Read());
                if (!added.Contains(c))
                {
                    Alphabet.Add(c);
                    added.Add(c);
                }
            }
        }

        private static void Encode(StreamReader input, StreamWriter output)
        {
            //int min_c = Alphabet.Min();
            while (input.BaseStream.Position != input.BaseStream.Length)
            {
                char c = Convert.ToChar(input.Read());
                int c_index = Alphabet.IndexOf(c);
                output.Write(Convert.ToChar(c_index));
                Alphabet.RemoveAt(c_index);
                Alphabet.Insert(0, c);
            }
        }
    }
}
