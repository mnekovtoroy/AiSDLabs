using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab3
{
    public class CompressingSequence
    {
        public delegate void CompressingAlg(string input_file, string output_file);

        public List<CompressingAlg> CompressingAlgList { get; set; }

        public CompressingSequence()
        {
            CompressingAlgList = new List<CompressingAlg>();
        }

        public void ApplyAlgrorithms(string input_absolute_path, string output_absolute_path)
        {
            string temp_file1 = output_absolute_path + ".temp1";
            string temp_file2 = output_absolute_path + ".temp2";
            CompressingAlgList[0].Invoke(input_absolute_path, temp_file1);
            for(int i = 1; i < CompressingAlgList.Count; i++)
            {
                CompressingAlgList[i].Invoke(temp_file1, temp_file2);
                var temp = temp_file1;
                temp_file1 = temp_file2;
                temp_file2 = temp;
            }
            if(File.Exists(output_absolute_path))
            {
                File.Delete(output_absolute_path);
            }
            File.Delete(temp_file2);
            File.Move(temp_file1, output_absolute_path);
            File.Delete(temp_file2);
        }
    }
}
