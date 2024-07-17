using UnityEngine;

namespace PerikDiceRoller
{
    public class UniDiceFace : IUniDiceFace
    {
        private IDiceFace _face;
        private Sprite _sprite;
        
        public Sprite Sprite { get => _sprite; }

        public int Value => _face.Value;

        public int TotalDrops => _face.TotalDrops;

        public UniDiceFace(int value, Sprite sprite)
        {
            _face = new DiceFace(value);
            _sprite = sprite;
        }
        public void SetSprite(Sprite sprite) => _sprite = sprite;

        public void AddDrop()
        {
            _face.AddDrop();
        }

        public void ResetDrops()
        {
            _face.ResetDrops();
        }

        public void SetValue(int value)
        {
            _face.SetValue(value);
        }
    }
}