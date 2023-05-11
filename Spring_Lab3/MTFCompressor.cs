namespace Spring_Lab3
{
    public static class MTFCompressor
    {
        private static Byte[] Alphabet { get; set; } = new Byte[256];

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
                for (int i = c_index; i > 0; i--)
                {
                    Alphabet[i] = Alphabet[i - 1];
                }
                Alphabet[0] = c;
            }
        }

        private static void Decode(BinaryReader input, BinaryWriter output)
        {
            for (int i = 0; i < 256; i++)
            {
                Alphabet[i] = Convert.ToByte(i);
            }

            while (input.BaseStream.Position != input.BaseStream.Length)
            {
                int c_index = Convert.ToInt32(input.ReadByte());
                Byte c = Alphabet[c_index];
                output.Write(c);
                for (int i = c_index; i > 0; i--)
                {
                    Alphabet[i] = Alphabet[i - 1];
                }
                Alphabet[0] = c;
            }
        }
    }
}
