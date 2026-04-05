namespace StudentSimulator.University.Practises.InterectiveTypes
{
    public static class Search
    {
        public static int BinarySearch(int[] array, int target)
        {
            int left = 0;
            int right = array.Length-1;
            int mid = 0;

            while(left <= right)
            {
                mid = (left + right) / 2;
                if (array[mid] == target)
                {
                    return mid;
                }
                else if (array[mid] < target)
                {
                    left = mid + 1;
                }
                else if (array[mid] > target)
                {
                    right = mid - 1;
                }
            }

            return -1;
        }

        public static int InterpolationSearch(int[] array, int target)
        {
            int left = 0;
            int right = array.Length - 1;
            int pos = 0;

            while (left <= right && target >= array[left] && target <= array[right])
            {
                pos = left + ((target - array[left]) * (right - left)) / (array[right] - array[left]);
                if (array[pos] == target)
                {
                    return pos;
                }
                else if (array[pos] < target)
                {
                    left = pos + 1;
                }
                else if (array[pos] > target)
                {
                    right = pos - 1;
                }
            }

            return -1;
        }

        public static int ExponentialSearch(int[] array, int target)
        {
            if(array[0] == target)
            {
                return 0;
            }

            int i = 1;
            while(i < array.Length && array[i] <= target)
            {
                i*=2;
            }

            return BinarySearch(i/2, Math.Min(i, array[array.Length - 1]), array, target);
        }

        private static int BinarySearch(int indexStart, int indexEnd, int[] array, int target)
        {
            int left = indexStart;
            int right = indexEnd;
            int mid = 0;

            while(left <= right)
            {
                mid = (left + right) / 2;
                if (array[mid] == target)
                {
                    return mid;
                }
                else if (array[mid] < target)
                {
                    left = mid + 1;
                }
                else if (array[mid] > target)
                {
                    right = mid - 1;
                }
            }

            return -1;
        }
    }
}