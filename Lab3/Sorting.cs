namespace Lab3
{
    public static class Sorting
    {
        static private void swap(ref int a, ref int b)
        {
            int t = a;
            a = b;
            b = t;
        }

        static public void InsertionSort(int[] array)
        {
            for(int i = 1; i < array.Length; i++)
            {
                int j = i;
                while(j > 0 && array[j] < array[j - 1])
                {
                    swap(ref array[j], ref array[j - 1]);
                    j--;
                }
            }
        }

        static public void SelectionSort(int[] array)
        {
            for(int i = 0; i < array.Length - 1; i++)
            {
                int curr_min_index = i;
                for(int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[curr_min_index])
                    {
                        curr_min_index = j;
                    }
                }
                swap(ref array[i], ref array[curr_min_index]);
            }
        }

        static public void BubbleSort(int[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                for(int j = 0; j < array.Length-i-1; j++)
                {
                    if(array[j] > array[j+1])
                    {
                        swap(ref array[j], ref array[j + 1]);
                    }
                }
            }
        }

        static public void MergeSort(int[] array)
        {
            MergeSort(array, 0, -1);
        }

        static public void MergeSort(int[] array, int left_border, int right_border, int[] buff = null)
        {
            //At the start
            if(right_border == -1)
            {
                right_border = array.Length - 1;
                buff = new int[array.Length];
            }
            //Recursion exit points
            if(right_border - left_border == 0)
            {
                return;
            }
            if(right_border - left_border == 1)
            {
                if(array[right_border] < array[left_border])
                {
                    swap(ref array[right_border], ref array[left_border]);
                }
                return;
            }
            //Recursion
            int middle = left_border + (right_border - left_border) / 2;
            MergeSort(array, left_border, middle, buff);
            MergeSort(array, middle + 1, right_border, buff);
            //Merge
            int left_curr_index = left_border;
            int right_curr_index = middle + 1;
            for(int i = left_border; i < right_border + 1; i++)
            {
                if((left_curr_index < middle + 1) && (right_curr_index < right_border + 1))
                {
                    if (array[right_curr_index] < array[left_curr_index])
                    {
                        buff[i] = array[right_curr_index];
                        right_curr_index++;
                    }
                    else
                    {
                        buff[i] = array[left_curr_index];
                        left_curr_index++;
                    }
                }
                else if(left_curr_index < middle + 1)
                {
                    buff[i] = array[left_curr_index];
                    left_curr_index++;
                }
                else if(right_curr_index < right_border + 1)
                {
                    buff[i] = array[right_curr_index];
                    right_curr_index++;
                }
            }
            //Copy into original array
            for(int i = left_border; i < right_border + 1; i++)
            {
                array[i] = buff[i];
            }
        }

        static public void ShellSort(int[] array)
        {
            var distance = array.Length / 2;
            while(distance > 0)
            {
                for(int i = distance; i < array.Length; i++)
                {
                    int j = i;
                    while (j >= distance && array[j] < array[j - distance])
                    {
                        swap(ref array[j], ref array[j - distance]);
                        j -= distance;
                    }
                }
                distance--;
            }
        }

        static public void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }

        static public void QuickSort(int[] array, int left_border, int right_border)
        {
            //At the start
            if (right_border == -1)
            {
                right_border = array.Length - 1;
            }
            //recursion exit
            if (left_border >= right_border)
            {
                return;
            }
            //initial pointers
            int left_pointer = left_border;
            int right_pointer = right_border;
            int reference = array[left_border + (right_border - left_border) / 2];
            //divide array
            while(left_pointer <= right_pointer)
            {
                while (array[left_pointer] < reference)
                {
                    left_pointer++;
                }
                while (array[right_pointer] > reference)
                {
                    right_pointer--;
                }
                if (left_pointer <= right_pointer)
                {
                    swap(ref array[left_pointer], ref array[right_pointer]);
                    left_pointer++;
                    right_pointer--;
                }
            }
            //recursion
            QuickSort(array, left_border, right_pointer);
            QuickSort(array, left_pointer, right_border);
        }
    }
}
