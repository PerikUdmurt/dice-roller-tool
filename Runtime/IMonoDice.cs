using UnityEngine;

namespace PerikDiceRoller
{
    public interface IMonoDice: IDice 
    {
        IUniDiceFace AddFace(int value, Sprite sprite, double chance);
        void SetFaceSprite(IUniDiceFace face,Sprite sprite);
        void SetSprite(Sprite sprite);
    }
}