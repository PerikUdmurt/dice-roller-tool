using UnityEngine;

namespace PerikDiceRoller
{
    public interface IUniDiceFace : IDiceFace
    {
        Sprite Sprite { get; }

        void SetSprite(Sprite sprite);
    }
}