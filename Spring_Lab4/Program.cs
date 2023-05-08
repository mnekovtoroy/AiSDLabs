using System.Drawing;

namespace Spring_Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*string input = "16x16.png";
            string output = "16x16.myjpeg";
            string output_decoded = "16x16_decoded.bmp";*/

            string input = "sample_1920×1280.bmp";
            string output = "sample_1920×1280";
            string output_decoded = "sample_1920×1280_decoded";
            Console.WriteLine("Starting...");
            for(int i = 1; i <= 10; i++)
            {
                MyJPEGConverter.ConvertToMyJPEG(input, output + $"_acc{i}.myjpeg", i);
                Console.WriteLine($"Compression {i} done!");
                MyJPEGConverter.ConvertToBMP(output + $"_acc{i}.myjpeg", output_decoded + $"_acc{i}.bmp");
                Console.WriteLine($"Decompression {i} done!");
            }
            Console.WriteLine("Done!");

            /*byte[,] test = new byte[8, 8]
            {
                {255, 255, 3, 4, 5, 6, 7, 8},
                {255, 255, 11, 12, 13, 14, 15, 16},
                {17, 18, 19, 20, 21, 22, 23, 24 },
                {25, 26, 27, 28, 29, 30, 31, 32},
                {33, 34, 35, 36, 37, 38, 39, 40 },
                {41, 42, 43, 44, 45, 46, 47, 48 },
                {49, 50, 51, 52, 53, 54, 55, 56 },
                {57, 58, 59, 60, 61, 62, 63, 64 }
            };
            test = MyJPEGConverter.Subsampling(test);
            Console.WriteLine();*/

            /*int[,] test = new int[8, 8]
            {
                {1, 2, 3, 4, 5, 6, 7, 8},
                {9, 10, 11, 12, 13, 14, 15, 16},
                {17, 18, 19, 20, 21, 22, 23, 24 },
                {25, 26, 27, 28, 29, 30, 31, 32},
                {33, 34, 35, 36, 37, 38, 39, 40 },
                {41, 42, 43, 44, 45, 46, 47, 48 },
                {49, 50, 51, 52, 53, 54, 55, 56 },
                {57, 58, 59, 60, 61, 62, 63, 64 }
            };
            int[,] test_2 = MyJPEGConverter.DCT(test);
            test_2 = MyJPEGConverter.Quantization(test_2, 8);
            byte[] test_3 = MyJPEGConverter.RLE(test_2);
            using (BinaryWriter bw = new BinaryWriter(new FileStream("test", FileMode.Create)))
            {
                bw.Write(test_3);
            }
            List<short> block_str = new List<short>();
            using (BinaryReader input = new BinaryReader(new FileStream("test", FileMode.Open)))
            {
                //decoding RLE
                while (block_str.Count < 64)
                {
                    short curr = input.ReadInt16();
                    if (curr == short.MaxValue)
                    {
                        byte counter = input.ReadByte();
                        short c = input.ReadInt16();
                        while (counter > 0)
                        {
                            block_str.Add(c);
                            counter--;
                        }
                    }
                    else
                    {
                        block_str.Add(curr);
                    }
                }
            }
            test_2 = MyJPEGConverter.ZigZagFill(block_str);
            test_2 = MyJPEGConverter.ReverseQuantization(test_2, 8);
            int[,] test_test = MyJPEGConverter.ReverseDCT(test_2);*/


            /* Color pixel = Color.FromArgb(57, 27, 76);
             double r = Convert.ToDouble(pixel.R);
             double g = Convert.ToDouble(pixel.G);
             double b = Convert.ToDouble(pixel.B);
             Byte Y = Convert.ToByte(0.299 * r + 0.587 * g + 0.114 * b);
             Byte Cb = Convert.ToByte(-0.1687 * r - 0.3313 * g + 0.5 * b + 128.0);
             Byte Cr = Convert.ToByte(0.5 * r - 0.4187 * g - 0.0813 * b + 128.0);

             int R = Math.Max(0, Math.Min(255, Convert.ToInt32(Y * 1.0 + (Cb - 128) * 0.0 + (Cr - 128) * 1.402)));
             int G = Math.Max(0, Math.Min(255, Convert.ToInt32(Y * 1.0 - (Cb - 128) * 0.34414 - (Cr - 128) * 0.71414)));
             int B = Math.Max(0, Math.Min(255, Convert.ToInt32(Y * 1.0 + (Cb - 128) * 1.772 - (Cr - 128) * 0.0)));

             Color pixel_decoded = Color.FromArgb(R, G, B);*/
        }
    }
}