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

        private static int BufferSize = 1024; //1kb

        public static void Compress(string input_file, string output_file)
        {
            using (BinaryReader input = new BinaryReader(new FileStream(input_file, FileMode.Open)))
            using (BinaryWriter output = new BinaryWriter(new FileStream(output_file, FileMode.Create)))
            {
                Byte[] buffer = new Byte[BufferSize];
                while(input.BaseStream.Position < input.BaseStream.Length)
                {
                    int bytesRead = input.Read(buffer, 0, BufferSize);
                    Byte[] data = new byte[bytesRead];
                    Array.Copy(buffer, data, bytesRead);
                    short original_index;
                    Byte[] output_data = Encode(data, out original_index);
                    output.Write(output_data);
                    output.Write(original_index);
                }
            }
        }

        public static void Decompress(string input_file, string output_file)
        {
            using (BinaryReader input = new BinaryReader(new FileStream(input_file, FileMode.Open)))
            using (BinaryWriter output = new BinaryWriter(new FileStream(output_file, FileMode.Create)))
            {
                Byte[] buffer = new Byte[BufferSize + 2];
                while (input.BaseStream.Position < input.BaseStream.Length)
                {
                    int bytesRead = input.Read(buffer, 0, BufferSize + 2);
                    Byte[] data = new byte[bytesRead - 2];
                    Array.Copy(buffer, data, bytesRead - 2);
                    short original_index = BitConverter.ToInt16(buffer, bytesRead - 2);
                    Byte[] output_data = Decode(data, original_index);
                    output.Write(output_data);
                }
            }
        }

        private static Byte[] Encode(Byte[] input, out short original_index)
        {
            int length = input.Length;
            List<Byte[]> rotations = new List<Byte[]>(length);
            for(int i = 0; i < length; i++)
            {
                rotations.Add(new Byte[length]);
                Array.Copy(input, i, rotations[i], 0, length - i);
                Array.Copy(input, 0, rotations[i], length - i, i);
            }
            rotations = rotations.OrderBy(s => s, new ByteArrayComparer()).ToList();
            Byte[] result = rotations.Select(s => s[length - 1]).ToArray();
            original_index = Convert.ToInt16(rotations.FindIndex(s => new ByteArrayComparer().Compare(s, input) == 0));
            return result;
        }

        private static Byte[] Decode(Byte[] input, int original_index)
        {
            int[] count = new int[256];
            HashSet<Byte> appeard = new HashSet<Byte>();
            //Build a vector
            List<int> vector_T = new List<int>();
            for(int i = 0; i < input.Length; i++)
            {
                count[Convert.ToInt32(input[i])]++;
            }
            int sum = 0;
            for (int i = 0; i < 256; i++)
            {
                if (count[i] != 0)
                {
                    sum += count[i];
                    count[i] = sum - count[i];
                }
            }
            for (int i = 0; i < input.Length; i++)
            {
                vector_T.Add(count[Convert.ToInt32(input[i])]);
                count[Convert.ToInt32(input[i])]++;
            }
            //Decoding
            Byte[] result = new Byte[input.Length];
            int pos = vector_T[original_index];
            for(int i = 1; i < input.Length; i++)
            {
                result[input.Length - i - 1] = input[pos];
                pos = vector_T[pos];
            }
            result[result.Length - 1] = input[original_index];
            return result;
        }
    }
}
