using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab3
{
    public static class HuffmanTree
    {
        private static HuffmanNode Root { get; set; }
        private static int TailSize { get; set; }

        public static void Compress(string input_file, string output_file)
        {
            using (BinaryReader input = new BinaryReader(new FileStream(input_file, FileMode.Open)))
            {
                BuildTree(input);
            }
            using (BinaryReader input = new BinaryReader(new FileStream(input_file, FileMode.Open)))
            using (BinaryWriter output = new BinaryWriter(new FileStream(output_file, FileMode.Create)))
            {
                Encode(input, output);
            }
        }

        public static void Decompress(string input_file, string output_file, Byte[] tree = null, int tail_size = -1)
        {
            tail_size = tail_size == -1 ? TailSize : tail_size;
            Root = tree == null ? Root : BuildTree(tree);
            using (BinaryReader input = new BinaryReader(new FileStream(input_file, FileMode.Open)))
            using (BinaryWriter output = new BinaryWriter(new FileStream(output_file, FileMode.Create)))
            {
                Decode(input, output, tail_size);
            }
        }

        private static void BuildTree(BinaryReader input)
        {
            //Counting frequencies
            var frequencies = new Dictionary<Byte, int>();
            while(input.BaseStream.Position < input.BaseStream.Length)
            {
                Byte c = input.ReadByte();
                if (!frequencies.ContainsKey(c))
                {
                    frequencies.Add(c, 0);
                }
                frequencies[c]++;
            }
            //creating huffmannodes
            var nodes = new List<HuffmanNode>();
            foreach(KeyValuePair<Byte,int> c in frequencies)
            {
                nodes.Add(new HuffmanNode()
                {
                    Character = c.Key,
                    Frequency = c.Value
                });
            }

            //building a tree
            while(nodes.Count > 1)
            {
                nodes = nodes.OrderBy(c => c.Frequency).ToList();

                HuffmanNode node = new HuffmanNode()
                {
                    Character = null,
                    Frequency = nodes[0].Frequency + nodes[1].Frequency,
                    Left = nodes[0],
                    Right = nodes[1]
                };

                nodes.RemoveRange(0, 2);
                nodes.Add(node);
            }
            Root = nodes.FirstOrDefault();
        }

        public static Byte[] SerializeTree()
        {
            return Root.Serialize();
        }

        public static int GetTailSize()
        {
            return TailSize;
        }

        private static HuffmanNode BuildTree(Byte[] encoded_tree)
        {
            if(encoded_tree.Length == 1)
            {
                if (encoded_tree[0] == 251)
                {
                    return null;
                } else
                {
                    return new HuffmanNode()
                    {
                        Character = encoded_tree[0],
                        Left = null,
                        Right = null
                    };
                }
            }
            HuffmanNode node = new HuffmanNode()
            {
                Character = null
            };
            int left_node_start = 1;
            int right_node_start;

            int bracket_count = 0;
            int index = 1;
            do
            {
                if (encoded_tree[index] == 252) { bracket_count++; }
                if (encoded_tree[index] == 253) { bracket_count--; }
                index++;
            } while (bracket_count != 0);
            right_node_start = index;

            Byte[] left_encoded = new Byte[right_node_start - left_node_start];
            Array.Copy(encoded_tree, left_node_start, left_encoded, 0, left_encoded.Length);

            Byte[] right_encoded = new Byte[encoded_tree.Length - 1 - right_node_start];
            Array.Copy(encoded_tree, right_node_start, right_encoded, 0, right_encoded.Length);

            node.Left = BuildTree(left_encoded);
            node.Right = BuildTree(right_encoded);

            return node;
        }

        private static void Encode(BinaryReader input, BinaryWriter output)
        {
            //Actually encode
            List<bool> encodedMaterial = new List<bool>();

            while (input.BaseStream.Position < input.BaseStream.Length)
            {
                //encode every character one by one
                Byte c = input.ReadByte();
                List<bool> encodedSymbol = Root.Traverse(c);
                if(encodedSymbol == null)
                {
                    throw new ArgumentException($"Encode: symbol {c} failed to encode.");
                }
                encodedMaterial.AddRange(encodedSymbol);
                //we have at least one encoded character
                if(encodedMaterial.Count >= 8)
                {
                    BitArray bit_c = new BitArray(encodedMaterial.GetRange(0, 8).ToArray());
                    byte[] byte_c = new byte[1];
                    bit_c.CopyTo(byte_c, 0);
                    output.Write(byte_c[0]);
                    encodedMaterial.RemoveRange(0, 8);
                }
            }
            //write whatever left
            int tail_size = 0;
            if (encodedMaterial.Count > 0)
            {
                if (encodedMaterial.Count < 8)
                {
                    tail_size = 8 - encodedMaterial.Count;
                    encodedMaterial.AddRange(new bool[tail_size]);
                }
                BitArray leftover_bits = new BitArray(encodedMaterial.GetRange(0, 8).ToArray());
                byte[] byte_c = new byte[1];
                leftover_bits.CopyTo(byte_c, 0);
                output.Write(byte_c[0]);
            }
            TailSize = tail_size;
        }

        private static void Decode(BinaryReader input, BinaryWriter output, int tail_size)
        {
            List<bool> input_bits = new List<bool>();

            Byte[] buffer = new byte[64];
            int bytes_read = input.Read(buffer, 0, 64);
            Byte[] bytes = new Byte[bytes_read];
            Array.Copy(buffer, bytes, bytes_read);
            BitArray bitArray = new BitArray(bytes);
            for (int i = 0; i < bitArray.Length; i++)
            {
                input_bits.Add(bitArray[i]);
            }
            if (input.BaseStream.Position == input.BaseStream.Length)
            {
                input_bits = input_bits.GetRange(0, input_bits.Count - tail_size);
            }
            while (input.BaseStream.Position < input.BaseStream.Length)
            {
                if(input_bits.Count < 256)
                {
                    bytes_read = input.Read(buffer, 0, 32);
                    bytes = new Byte[bytes_read];
                    Array.Copy(buffer, bytes, bytes_read);
                    bitArray = new BitArray(bytes);
                    for (int i = 0; i < bitArray.Length; i++)
                    {
                        input_bits.Add(bitArray[i]);
                    }
                    if(input.BaseStream.Position == input.BaseStream.Length)
                    {
                        input_bits = input_bits.GetRange(0, input_bits.Count - tail_size);
                    }
                }
                Byte? output_byte = Root.GetByte(input_bits);
                if (!output_byte.HasValue)
                {
                    throw new InvalidDataException("Decode: Data is not decodable");
                }
                output.Write(output_byte.Value);
            }
            while(input_bits.Count > 0)
            {
                Byte? output_byte = Root.GetByte(input_bits);
                if (!output_byte.HasValue)
                {
                    throw new InvalidDataException("Decode: Data is not decodable");
                }
                output.Write(output_byte.Value);
            }
        }
    }
}
