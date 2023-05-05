using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab3
{
    public static class BWTCompressor
    {
        public class ByteArrayComparer : IComparer<ICollection<byte>>
        {
            public int Compare(ICollection<byte> x, ICollection<byte> y)
            {
                int result;
                for (int index = 0; index < Math.Min(x.Count, y.Count); index++)
                {
                    result = x.ElementAt(index).CompareTo(y.ElementAt(index));
                    if (result != 0) return result;
                }
                return x.Count.CompareTo(y.Count);
            }
        }

        public static void Compress(string input_file, string output_file)
        {
            using (BinaryReader input = new BinaryReader(new FileStream(input_file, FileMode.Open)))
            using (BinaryWriter output = new BinaryWriter(new FileStream(output_file, FileMode.Create)))
            {
                int buffer_size = 1024; // 1kb
                //output.Write($"{buffer_size}_");
                Byte[] buffer = new Byte[buffer_size];
                while(input.BaseStream.Position < input.BaseStream.Length)
                {
                    int bytesRead = input.Read(buffer, 0, buffer_size);
                    Byte[] data = new byte[bytesRead];
                    Array.Copy(buffer, data, bytesRead);
                    //string data = new string(buffer, 0, bytesRead);
                    short original_index;
                    Byte[] output_data = Encode(data, out original_index);
                    output.Write(output_data);
                    output.Write(original_index);
                }
            }
        }

        private static Byte[] Encode(Byte[] input, out short original_index)
        {
            int length = input.Length;
            List<Byte[]> rotations = new List<Byte[]>(length);
            for(int i = 0; i < length; i++)
            {
                //rotations[i] = input.Substring(i) + input.Substring(0, i);
                rotations.Add(new Byte[length]);
                Array.Copy(input, i, rotations[i], 0, length - i);
                Array.Copy(input, 0, rotations[i], length - i, i);
            }
            rotations = rotations.OrderBy(s => s, new ByteArrayComparer()).ToList();
            Byte[] result = rotations.Select(s => s[length - 1]).ToArray();
            original_index = Convert.ToInt16(rotations.FindIndex(s => new ByteArrayComparer().Compare(s, input) == 0));
            return result;
        }
    }
}
