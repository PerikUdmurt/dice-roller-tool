namespace PerikDiceRoller
{
    public interface IDiceFace
    {
        int Value { get; }
        int TotalDrops { get; }
        void SetValue(int value);
        void AddDrop();
        void ResetDrops();
    }
}