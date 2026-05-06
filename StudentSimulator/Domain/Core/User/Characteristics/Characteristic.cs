namespace StudentSimulator.Domain.Core.User.Characteristics
{
    public class Characteristic
    {
        public double Value {get; set;}
        public double MaxValue {get; set;}
        public double MinValue {get; set;}

        public Characteristic(double value, double maxValue, double minValue)
        {
            Value = value;
            MaxValue = maxValue;
            MinValue = minValue;
        }

        public bool IncreaseValue(double value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Значення не може бути менше 0!");
            }
            if(Value >= MaxValue)
            {
                return false;
            }

            Value = Math.Min(Value + value, MaxValue);
            return true;
        }

        public bool DecreaseValue(double value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Значення не може бути менше 0!");
            }

            if (Value <= MinValue)
            {
                return false;
            }

            Value = Math.Max(Value - value, MinValue);
            return true;
        }
    }
}