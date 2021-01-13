namespace Unicorn
{
    public class Calculator
    {
        public int Add(int x, int y) => x + y;
        public int Subtract(int x, int y) => x - y;
        public int Multiply(int x, int y) => y * x;
        public int Divide(int numerator, int denominator)
        {
            return numerator / denominator;
        }
    }
}