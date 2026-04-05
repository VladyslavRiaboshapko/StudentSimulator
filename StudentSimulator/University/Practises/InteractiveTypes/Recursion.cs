namespace StudentSimulator.University.Practises.InterectiveTypes
{
    public static class Recursion
    {
        public static int Factorial(int x)
        {
            if(x <= 1)
            {
                return 1;
            }
            return x * Factorial(x - 1);
        }
    }
}