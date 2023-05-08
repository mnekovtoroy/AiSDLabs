using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab3
{
    public static class LZ78Compressor
    {
        private static Byte separator = 0;

        public static void Compress(string input_file, string output_file)
        {
            using (BinaryReader input = new BinaryReader(new FileStream(input_file, FileMode.Open)))
            using (BinaryWriter output = new BinaryWriter(new FileStream(output_file, FileMode.Create)))
            {
                Encode(input, output);
            }
        }

        public static void Decompress(string input_file, string output_file)
        {
            using (BinaryReader input = new BinaryReader(new FileStream(input_file, FileMode.Open)))
            using (BinaryWriter output = new BinaryWriter(new FileStream(output_file, FileMode.Create)))
            {
                Decode(input, output);
            }
        }

        private static void Encode(BinaryReader input, BinaryWriter output)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            string buffer = "";
            while (input.BaseStream.Position < input.BaseStream.Length)
            {
                Byte curr_b = input.ReadByte();
                string curr = $"{Convert.ToChar(curr_b)}";
                if (dict.ContainsKey(buffer + curr))
                {
                    buffer += curr;
                }
                else
                {
                    int index = dict.ContainsKey(buffer) ? dict[buffer] : 0;
                    output.Write(index);
                    output.Write(separator);
                    output.Write(curr_b);
                    dict.Add(buffer + curr, dict.Count + 1);
                    buffer = "";
                }
            }
            if (!string.IsNullOrEmpty(buffer))
            {
                output.Write(dict[buffer]);
                output.Write(separator);
                output.Write(separator);
            }
        }

        private static void Decode(BinaryReader input, BinaryWriter output)
        {
            Dictionary<int, Byte[]> dict = new Dictionary<int, Byte[]>();
            int index = 0;

            while(input.BaseStream.Position < input.BaseStream.Length)
            {
                int curr_index = input.ReadInt32();
                input.ReadByte(); //separator
                Byte curr_b = input.ReadByte();

                if(curr_index > 0)
                {
                    output.Write(dict[curr_index]);
                }
                if (curr_b != separator) { output.Write(curr_b); }
                Byte[] toDict = curr_index == 0 ? new Byte[0] : dict[curr_index];
                dict.Add(dict.Count + 1, toDict.Append(curr_b).ToArray());
            }
        }

        /*private static bool IsEqual(List<Byte> list1, List<Byte> list2)
        {
            if(list1.Count != list2.Count) return false;
            for(int i = 0; i < list1.Count; i++)
            {
                if(list1[i] != list2[i]) return false;
            }
            return true;
        }*/
    }
}
