using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab3
{
    public static class BWTCompressor
    {
        public static void Compress(string input_file, string output_file)
        {
            using (StreamReader input = new StreamReader(input_file))
            using (StreamWriter output = new StreamWriter(output_file))
            {
                int buffer_size = 1024;
                //output.Write($"{buffer_size}_");
                char[] buffer = new char[buffer_size];
                while(!input.EndOfStream)
                {
                    int bytesRead = input.Read(buffer, 0, buffer_size);
                    string data = new string(buffer, 0, bytesRead);
                    string output_data = Encode(data);
                    output.Write(output_data);
                }
            }
        }

        private static string Encode(string input)
        {
            int length = input.Length;
            string[] rotations = new string[length];
            for(int i = 0; i < length; i++)
            {
                rotations[i] = input.Substring(i) + input.Substring(0, i);
            }
            rotations = rotations.OrderBy(s => s).ToArray();
            string result = new string(rotations.Select(s => s[length - 1]).ToArray());
            return result;
        }
    }
}
