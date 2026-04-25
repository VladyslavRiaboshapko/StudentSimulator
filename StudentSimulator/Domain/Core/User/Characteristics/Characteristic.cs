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
            if(Value + value < MaxValue)
            {
                Value += value;
                return true;
            }

            return false;
        }

        public bool DecreaseValue(double value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Значення не може бути менше 0!");
            }

            if(Value - value > MinValue)
            {
                Value -= value;
                return true;
            }

            return false;
        }
    }
}