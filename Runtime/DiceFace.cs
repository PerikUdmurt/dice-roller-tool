namespace PerikDiceRoller
{
    public class DiceFace: IDiceFace
    {
        private int _value;
        private int _totalDrops;
        public int Value { get => _value; }
        public int TotalDrops { get => _totalDrops; }

        public DiceFace(int value) 
        { 
            _value = value;
        }
        public void AddDrop() => _totalDrops++;

        public void SetValue(int value) => _value = value;

        public void ResetDrops()
        {
            _totalDrops = 0;
        }
    }
}