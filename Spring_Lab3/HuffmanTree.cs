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
        private static List<HuffmanNode> _nodes;
        private static HuffmanNode Root { get; set; }
        private static Dictionary<Byte, int> Frequencies { get; set; }

        public static void Compress(string input_file, string output_file)
        {
            using (BinaryReader input_sr1 = new BinaryReader(new FileStream(input_file, FileMode.Open)))
            {
                BuildTree(input_sr1);
            }
            using (BinaryReader input_sr2 = new BinaryReader(new FileStream(input_file, FileMode.Open)))
            using (BinaryWriter output_sw = new BinaryWriter(new FileStream(output_file, FileMode.Create)))
            {
                Encode(input_sr2, output_sw);
            }
        }

        private static void BuildTree(BinaryReader input)
        {
            //Counting frequencies
            Frequencies = new Dictionary<Byte, int>();
            while(input.BaseStream.Position < input.BaseStream.Length)
            {
                Byte c = input.ReadByte();
                if (!Frequencies.ContainsKey(c))
                {
                    Frequencies.Add(c, 0);
                }
                Frequencies[c]++;
            }
            //creating huffmannodes
            _nodes = new List<HuffmanNode>();
            foreach(KeyValuePair<Byte,int> c in Frequencies)
            {
                _nodes.Add(new HuffmanNode()
                {
                    Character = c.Key,
                    Frequency = c.Value
                });
            }

            //building a tree
            while(_nodes.Count > 1)
            {
                _nodes = _nodes.OrderBy(c => c.Frequency).ToList();

                HuffmanNode node = new HuffmanNode()
                {
                    Character = null,
                    Frequency = _nodes[0].Frequency + _nodes[1].Frequency,
                    Left = _nodes[0],
                    Right = _nodes[1]
                };

                _nodes.RemoveRange(0, 2);
                _nodes.Add(node);
            }
            Root = _nodes.FirstOrDefault();
        }

        private static void Encode(BinaryReader input, BinaryWriter output)
        {
            //Serialize the tree
            //string tree = Root.Serialize() + ";;";
            //output.Write(tree);

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

            //string tail_size_str = "";
            //if (tail_size < 10)
            //{
            //    tail_size_str += "0";
            //}
            //tail_size_str += $"{tail_size}";
            //output.Write(tail_size_str);
        }
    }
}
