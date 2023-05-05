using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab3
{
    public static class MTFCompressor
    {
        private static Byte[] Alphabet { get; set; } = new Byte[256];

        public static void Compress(string input_file, string output_file)
        {
            /*using (StreamReader input = new StreamReader(new FileStream(input_file, FileMode.Open), Encoding.UTF8))
            {
                InitializeAlphabet(input);
            }*/
            using (BinaryReader input = new BinaryReader(new FileStream(input_file, FileMode.Open)))
            using (BinaryWriter output = new BinaryWriter(new FileStream(output_file, FileMode.Create)))
            {
                Encode(input, output);
            }
        }

        private static void Encode(BinaryReader input, BinaryWriter output)
        {
            //int min_c = Alphabet.Min();
            for(int i = 0; i < 256; i++)
            {
                Alphabet[i] = Convert.ToByte(i);
            }

            while (input.BaseStream.Position != input.BaseStream.Length)
            {
                Byte c = input.ReadByte();
                int c_int = Array.IndexOf(Alphabet, c);
                Byte c_index = Convert.ToByte(Array.IndexOf(Alphabet, c));
                output.Write(c_index);
                //Alphabet.RemoveAt(c_index);
                //Alphabet.Insert(0, c);
                for (int i = c_index; i > 0; i--)
                {
                    Alphabet[i] = Alphabet[i - 1];
                }
                Alphabet[0] = c;
            }
        }

        /*private static void InitializeAlphabet(StreamReader input)
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
        }*/
    }
}
