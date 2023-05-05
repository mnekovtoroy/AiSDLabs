using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab3
{
    public static class TextCompression
    {
        public static void HA(string input_file, string output_file)
        {
            HuffmanTree.Compress(input_file, output_file);
        }

        public static void RLE(string input_file, string output_file)
        {
            RunLengthEncoding.Compress(input_file, output_file);
        }

        public static void LZ78(string input_file, string output_file)
        {
            LZ78Compressor.Compress(input_file, output_file);
        }

        public static void BWT(string input_file, string output_file)
        {
            BWTCompressor.Compress(input_file, output_file);
        }

        public static void MTF(string input_file, string output_file)
        {
            MTFCompressor.Compress(input_file, output_file);
        }

        public static void AC(string input_file, string output_file)
        {
            ArithmeticCoding.Compress(input_file, output_file);
        }
    }
}