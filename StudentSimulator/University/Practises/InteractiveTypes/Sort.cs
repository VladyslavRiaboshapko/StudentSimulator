namespace StudentSimulator.University.Practises.InterectiveTypes
{
    public static class Sort
    {
        public static int[] BubbleSort(int[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                for(int j = 0; j < array.Length-1; j++)
                {
                    if(array[j] > array[j+1])
                    {
                        int temp = array[j];
                        array[j] = array[j+1];
                        array[j+1] = temp;
                    }
                }
            }

            return array;
        }

        public static int[] InsertationSort(int[] array)
        {
            for(int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i-1;
                while (j >= 0 && array[j] > key)
                {
                    array[j+1] = array[j];
                    j--;
                }
                array[j+1] = key;
            }

            return array;
        }

        public static int[] CombSort(int[] array)
        {
            int gap = array.Length;
            bool swapped = true;
            double shrinkFactor = 1.3;
            while (gap > 1 || swapped)
            {
                gap = (int)(gap / shrinkFactor);
                if (gap < 1)
                {
                    gap = 1;
                }

                swapped = false;
                
                for (int i = 0; i + gap < array.Length; i++)
                {
                    if (array[i] > array[i + gap])
                    {
                        int temp = array[i];
                        array[i] = array[i + gap];
                        array[i + gap] = temp;
                        
                        swapped = true;
                    }
                }
            }
            return array;
        }

        public static int[] BucketSort(int[] array)
        {
            int min = array[0];
            int max = array[0];

            for(int i = 0; i < array.Length; i++)
            {
                if(array[i] < min)
                {
                    min = array[i];
                }
                if(array[i] > max)
                {
                    max = array[i];
                }
            }

            int k = (max-min) / 5;
            if(k < 1)
            {
                k = 1;
            }

            List<int>[] buckets = new List<int>[k];
            for(int i = 0; i < k; i++)
            {
                buckets[i] = new List<int>();
            }

            for(int i = 0; i < array.Length; i++)
            {
                if(array[i] == max)
                {
                    buckets[k-1].Add(array[i]);
                }
                else
                {
                    buckets[(array[i] - min) / 5].Add(array[i]);
                }
            }

            for(int i = 0; i < k; i++)
            {
                int[] temp = buckets[i].ToArray();
                InsertationSort(temp);
                buckets[i] = temp.ToList();
            }

            int[] newArr = new int[array.Length];
            int e = 0;

            for(int i = 0; i < k; i++)
            {
                for(int j = 0; j < buckets[i].Count; j++)
                {
                    newArr[e] = buckets[i][j];
                    e++;
                }
            }

            return newArr;
        }

        public static int[] MergeSort(int[] array)
        {
            if(array.Length <= 1)
            {
                return array;
            }
            
            int mid = array.Length / 2;
            int[] left = new int[mid];
            int[] right = new int[array.Length - mid];

            for(int i = 0; i < array.Length; i++)
            {
                if(i < mid)
                {
                    left[i] = array[i];
                }
                else
                {
                    right[i - mid] = array[i];
                }
            }

            left = MergeSort(left);
            right = MergeSort(right);

            return Merge(left, right);
        }

        private static int[] Merge(int[] left, int[] right)
        {
            int[] result = new int[left.Length + right.Length];
            int leftIndex = 0; 
            int rightIndex = 0;
            int resultIndex = 0;

            
            while (leftIndex < left.Length && rightIndex < right.Length)
            {
                if (left[leftIndex] <= right[rightIndex])
                {
                    result[resultIndex] = left[leftIndex];
                    leftIndex++;
                }
                else
                {
                    result[resultIndex] = right[rightIndex];
                    rightIndex++;
                }
                resultIndex++;
            }

            
            while (leftIndex < left.Length)
            {
                result[resultIndex] = left[leftIndex];
                leftIndex++;
                resultIndex++;
            }

            
            while (rightIndex < right.Length)
            {
                result[resultIndex] = right[rightIndex];
                rightIndex++;
                resultIndex++;
            }

            return result;
        }

        public static int[] QuickSort(int[] array, int left, int right)
        {
            if(left < right)
            {
                int pivotIndex = Patrition(array, left, right);

                QuickSort(array, left, pivotIndex - 1);
                QuickSort(array, pivotIndex + 1, right);
            }

            return array;
        }

        private static int Patrition(int[] array, int left, int right)
        {
            int pivot = array[right];

            int i = left - 1;

            for(int j = left; j < right; j++)
            {
                if(array[j] <= pivot)
                {
                    i++;
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            int tempPivot = array[i+1];
            array[i+1] = array[right];
            array[right] = tempPivot;

            return i + 1;
        }
    }
}