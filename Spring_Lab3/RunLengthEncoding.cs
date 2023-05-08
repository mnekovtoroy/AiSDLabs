using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab3
{
    public static class RunLengthEncoding
    {
        private static Byte Separator = 0;

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
            if (input.BaseStream.Position == input.BaseStream.Length) return;
            Byte last_c = input.ReadByte();
            short counter = 1;
            while(input.BaseStream.Position != input.BaseStream.Length)
            {
                Byte curr = input.ReadByte();
                if (curr != last_c)
                {
                    if(counter > 3)
                    {
                        output.Write(Separator);
                        output.Write(counter);
                        output.Write(last_c);
                    }
                    else
                    {
                        while(counter > 0)
                        {
                            output.Write(last_c);
                            counter--;
                        }
                    }
                    last_c = curr;
                    counter = 1;
                } else
                {
                    counter++;
                }
            }
            if (counter > 3)
            {
                output.Write(Separator);
                output.Write(counter);
                output.Write(last_c);
            }
            else
            {
                while (counter > 0)
                {
                    output.Write(last_c);
                    counter--;
                }
            }
        }

        private static void Decode(BinaryReader input, BinaryWriter output) 
        {
            while (input.BaseStream.Position < input.BaseStream.Length)
            {
                Byte curr = input.ReadByte();
                if (curr == Separator)
                {
                    short counter = input.ReadInt16();
                    Byte c = input.ReadByte();
                    while (counter > 0)
                    {
                        output.Write(c);
                        counter--;
                    }
                }
                else
                {
                    output.Write(curr);
                }
            }
        }
    }
}
