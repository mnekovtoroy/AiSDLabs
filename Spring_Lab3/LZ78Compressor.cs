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
            using (BinaryReader input = new BinaryReader(new FileStream(input_file, FileMode.Open)))
            using (StreamWriter output = new StreamWriter(output_file))
            {
                //input.BaseStream.Position = 0;
                //output.BaseStream.Position = 0;
                Encode(input, output);
            }
        }

        /*private static void Encode(BinaryReader input, BinaryWriter output)
        {
            Dictionary<Byte, int> dict = new Dictionary<Byte, int>();
            int index = 0;
            Byte separator = 0;

            while (input.BaseStream.Position < input.BaseStream.Length)
            {
                Byte curr = input.ReadByte();
                if(dict.ContainsKey(curr))
                {
                    index = dict[curr];
                } else
                {
                    dict.Add(curr, dict.Count + 1);
                    //output.Write($"({index},{curr})");
                    output.Write(index);
                    output.Write(separator);
                    output.Write(curr);
                    index = 0;
                }
            }

            if(index > 0)
            {
                //output.Write($"({index},)");
                output.Write(Convert.ToByte(index));
                output.Write(separator);
                output.Write(separator);
            }
        }*/

        private static void Encode(BinaryReader input, StreamWriter output)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            string buffer = ""; 
            //int index = 0;
            //Byte separator = 0;
            while (input.BaseStream.Position < input.BaseStream.Length)
            {
                string curr = Convert.ToChar(input.Read()).ToString();
                if (dict.ContainsKey(buffer + curr))
                {
                    buffer += curr;
                } else
                {
                    int index = dict.ContainsKey(buffer) ? dict[buffer] : 0;
                    output.Write($"({index},{curr})");
                    dict.Add(buffer + curr, dict.Count + 1);
                    buffer = "";
                }
            }
            if(!string.IsNullOrEmpty(buffer))
            {
                //string last_ch = buffer[buffer.Length - 1].ToString();
                //buffer = buffer.Substring(0, buffer.Length - 2);
                output.Write($"({dict[buffer]},)");
            }
        }
    }
}
