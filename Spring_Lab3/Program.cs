using static Spring_Lab3.ArithmeticCoding;

namespace Spring_Lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*BinaryReader reader = new BinaryReader(File.OpenRead("C:\\Users\\User\\source\\repos\\AiSDLabs\\Spring_Lab3\\tests\\enwik8_10p"));
            int[] counters = new int[256];
            while(reader.BaseStream.Position < reader.BaseStream.Length)
            {
                Byte nextb = reader.ReadByte();
                counters[Convert.ToInt32(nextb)]++;
            }
            reader.Close();*/
            /*string input = "input.txt";
            string output = "output.txt";
            string output_decoded = "output_decoded.txt";
            LZ78Compressor.Compress(input, output);
            Console.WriteLine("Compression done!");
            LZ78Compressor.Decompress(output, output_decoded);
            Console.WriteLine("Decompression done!");*/

            /*Dictionary<Byte, Interval> intervals;
            Byte[] input = new byte[] { 72, 101, 108, 108, 111, 32, 119, 111, 114, 108, 100, 33 };
            double output = ArithmeticCoding.Encode(input, out intervals);
            Byte[] output_decoded = ArithmeticCoding.Decode(output, intervals);
            Console.WriteLine("done");*/

            //enwik8 tests
            CompressingSequence[] sequences = new CompressingSequence[7];
            for (int i = 0; i < sequences.Length; i++)
            {
                sequences[i] = new CompressingSequence();
            }
            sequences[0].CompressingAlgList.Add(TextCompression.HA);

            sequences[1].CompressingAlgList.Add(TextCompression.AC);

            sequences[2].CompressingAlgList.Add(TextCompression.LZ78);

            sequences[3].CompressingAlgList.Add(TextCompression.BWT);
            sequences[3].CompressingAlgList.Add(TextCompression.MTF);
            sequences[3].CompressingAlgList.Add(TextCompression.HA);

            sequences[4].CompressingAlgList.Add(TextCompression.BWT);
            sequences[4].CompressingAlgList.Add(TextCompression.MTF);
            sequences[4].CompressingAlgList.Add(TextCompression.AC);

            sequences[5].CompressingAlgList.Add(TextCompression.RLE);
            sequences[5].CompressingAlgList.Add(TextCompression.BWT);
            sequences[5].CompressingAlgList.Add(TextCompression.MTF);
            sequences[5].CompressingAlgList.Add(TextCompression.RLE);
            sequences[5].CompressingAlgList.Add(TextCompression.HA);

            sequences[6].CompressingAlgList.Add(TextCompression.RLE);
            sequences[6].CompressingAlgList.Add(TextCompression.BWT);
            sequences[6].CompressingAlgList.Add(TextCompression.MTF);
            sequences[6].CompressingAlgList.Add(TextCompression.RLE);
            sequences[6].CompressingAlgList.Add(TextCompression.AC);

            Console.WriteLine("Starting...");
            string input_abs_path = "C:\\Users\\User\\source\\repos\\AiSDLabs\\Spring_Lab3\\tests\\enwik8_10p";
            string output_directory = "C:\\Users\\User\\source\\repos\\AiSDLabs\\Spring_Lab3\\tests\\";
            using (StreamWriter time_measurements = new StreamWriter(output_directory + "time_measurements.txt"))
            {
                time_measurements.WriteLine("test time");
                for (int i = 0; i < sequences.Length; i++)
                {
                    DateTime start = DateTime.Now;
                    sequences[i].ApplyAlgrorithms(input_abs_path, output_directory + $"test_{i + 1}");
                    TimeSpan CompressingDuration = DateTime.Now - start;
                    time_measurements.WriteLine($"{i + 1} {CompressingDuration.TotalSeconds}");
                    Console.WriteLine($"Test {i + 1} out of {sequences.Length} done");
                }
            }
            Console.WriteLine("Done");
        }
    }
}