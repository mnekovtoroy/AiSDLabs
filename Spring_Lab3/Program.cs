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
            /*string input = "C:\\Users\\User\\source\\repos\\AiSDLabs\\Spring_Lab3\\tests\\enwik8_10p";
            string output = "C:\\Users\\User\\source\\repos\\AiSDLabs\\Spring_Lab3\\tests\\test_3";
            TextCompression.LZ78(input, output);
            Console.WriteLine("Compression done!");*/

            //setting up tests
            //problem with MTF
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