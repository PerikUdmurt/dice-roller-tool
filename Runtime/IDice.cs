using System.Collections.Generic;

namespace PerikDiceRoller
{
    public interface IDice
    {
        Dictionary<IDiceFace, double> Faces { get; }

        void AddFace(IDiceFace face, double chance);
        void SetFaceValue(IDiceFace face, int value);
        void RemoveFace(IDiceFace face);
        void ResetChances();
        IDiceFace Roll();
        void SetFaceChance(IDiceFace face, double newChance);
        IDiceFace AddFace(int value, double chance);
    }
}