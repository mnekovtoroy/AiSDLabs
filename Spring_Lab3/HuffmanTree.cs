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
        private static Dictionary<char, int> Frequencies { get; set; }

        public static void Compress(string input_file, string output_file)
        {
            using (StreamReader input_sr1 = new StreamReader(input_file))
            {
                BuildTree(input_sr1);
            }
            using (StreamReader input_sr2 = new StreamReader(input_file))
            using (StreamWriter output_sw = new StreamWriter(new FileStream(output_file, FileMode.Create), Encoding.Unicode))
            {
                Encode(input_sr2, output_sw);
            }
        }

        private static void BuildTree(StreamReader input)
        {
            //Counting frequencies
            Frequencies = new Dictionary<char, int>();
            while(!input.EndOfStream)
            {
                char c = Convert.ToChar(input.Read());
                if (!Frequencies.ContainsKey(c))
                {
                    Frequencies.Add(c, 0);
                }
                Frequencies[c]++;
            }
            //creating huffmannodes
            _nodes = new List<HuffmanNode>();
            foreach(KeyValuePair<char,int> c in Frequencies)
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

        private static void Encode(StreamReader input, StreamWriter output)
        {
            //Serialize the tree
            //string tree = Root.Serialize() + ";;";
            //output.Write(tree);

            //Actually encode
            List<bool> encodedMaterial = new List<bool>();

            while (!input.EndOfStream)
            {
                //encode every character one by one
                char c = Convert.ToChar(input.Read());
                List<bool> encodedSymbol = Root.Traverse(c);
                if(encodedSymbol == null)
                {
                    throw new ArgumentException($"Encode: symbol {c} failed to encode.");
                }
                encodedMaterial.AddRange(encodedSymbol);
                //we have at least one encoded character
                if(encodedMaterial.Count >= 16)
                {
                    BitArray bit_c = new BitArray(encodedMaterial.GetRange(0, 16).ToArray());
                    byte[] byte_c = new byte[2];
                    bit_c.CopyTo(byte_c, 0);
                    char encoded_c = BitConverter.ToChar(byte_c, 0);
                    output.Write(encoded_c);
                    encodedMaterial.RemoveRange(0, 16);
                }
            }
            //write whatever left
            int tail_size = 0;
            if (encodedMaterial.Count > 0)
            {
                if (encodedMaterial.Count < 16)
                {
                    tail_size = 16 - encodedMaterial.Count;
                    encodedMaterial.AddRange(new bool[tail_size]);
                }
                BitArray leftover_bits = new BitArray(encodedMaterial.GetRange(0, 16).ToArray());
                byte[] byte_c = new byte[2];
                leftover_bits.CopyTo(byte_c, 0);
                char encoded_c = BitConverter.ToChar(byte_c, 0);
                output.Write(encoded_c);
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
