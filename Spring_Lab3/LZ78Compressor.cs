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
        public static void Compress(string input_file, string output_file)
        {
            using (BinaryReader input = new BinaryReader(new FileStream(input_file, FileMode.Open)))
            using (BinaryWriter output = new BinaryWriter(new FileStream(output_file, FileMode.Create)))
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

        /*private static void Encode(BinaryReader input, BinaryWriter output)
        {
            Byte separator = 0;
            //Dictionary<List<Byte>, int> dict = new Dictionary<List<Byte>, int>();
            //List<Byte> buffer = new List<Byte>();
            //List<Byte> curr = new List<Byte>();
            //int index = 0;
            //Byte separator = 0;

            while (input.BaseStream.Position < input.BaseStream.Length)
            {
                Byte curr = input.ReadByte();
                if (dict.Any(x => IsEqual(x.Key, buffer.Append(curr).ToList())))
                {
                    //buffer += curr;
                    buffer = buffer.Append(curr).ToList();
                } else
                {
                    //int index = dict.ContainsKey(buffer) ? dict[buffer] : 0;
                    int index = dict.Any(x => IsEqual(x.Key, buffer)) ?
                        dict.Where(x => IsEqual(x.Key, buffer)).ElementAt(0).Value : 0;
                    //output.Write($"({index},{curr})");
                    output.Write(index);
                    output.Write(separator);
                    output.Write(curr);
                    dict.Add(buffer.Append(curr).ToList(), dict.Count + 1);
                    buffer.Clear();
                }
            }
            if(buffer.Count > 0)
            {
                //string last_ch = buffer[buffer.Length - 1].ToString();
                //buffer = buffer.Substring(0, buffer.Length - 2);
                //output.Write($"({dict[buffer]},)");
                int index = dict.Any(x => IsEqual(x.Key, buffer)) ?
                        dict.Where(x => IsEqual(x.Key, buffer)).ElementAt(0).Value : 0;
                output.Write(index);
                output.Write(separator);
                output.Write(separator);
            }
        }*/

        private static void Encode(BinaryReader input, BinaryWriter output)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            string buffer = "";
            Byte separator = 0;
            while (input.BaseStream.Position < input.BaseStream.Length)
            {
                //string curr = Convert.ToChar(input.Read()).ToString();
                Byte curr_b = input.ReadByte();
                string curr = $"{Convert.ToChar(curr_b)}";
                if (dict.ContainsKey(buffer + curr))
                {
                    buffer += curr;
                }
                else
                {
                    int index = dict.ContainsKey(buffer) ? dict[buffer] : 0;
                    //output.Write($"({index},{curr})");
                    output.Write(index);
                    output.Write(separator);
                    output.Write(curr_b);
                    dict.Add(buffer + curr, dict.Count + 1);
                    buffer = "";
                }
            }
            if (!string.IsNullOrEmpty(buffer))
            {
                //string last_ch = buffer[buffer.Length - 1].ToString();
                //buffer = buffer.Substring(0, buffer.Length - 2);
                output.Write(dict[buffer]);
                output.Write(separator);
                output.Write(separator);
            }
        }

        private static bool IsEqual(List<Byte> list1, List<Byte> list2)
        {
            if(list1.Count != list2.Count) return false;
            for(int i = 0; i < list1.Count; i++)
            {
                if(list1[i] != list2[i]) return false;
            }
            return true;
        }
    }
}
