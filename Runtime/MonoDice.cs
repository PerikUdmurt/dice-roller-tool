using System.Collections.Generic;
using UnityEngine;

namespace PerikDiceRoller
{
    [RequireComponent(typeof(SpriteRenderer)), ExecuteInEditMode]
    public class MonoDice : MonoBehaviour, IMonoDice
    {
        public IDice dice = new Dice();

        [SerializeField] private SpriteRenderer _spriteRenderer;

        public Dictionary<IDiceFace, double> Faces => dice.Faces;

        public IDiceFace AddFace(int value, double chance )
        {
            return dice.AddFace(value, chance);
        }

        public IUniDiceFace AddFace(int value, Sprite sprite, double chance)
        {
            IUniDiceFace face = new UniDiceFace(value, sprite);
            dice.AddFace(face, chance);
            return face;
        }

        public void AddFace(IDiceFace face, double chance)
        {
            dice.AddFace(face, chance);
        }

        public void SetFaceValue(IDiceFace face, int value)
        {
            dice.SetFaceValue(face, value);
        }

        public void RemoveFace(IDiceFace face)
        {
            dice.RemoveFace(face);
        }

        public void ResetChances()
        {
            dice.ResetChances();
        }

        public IDiceFace Roll()
        {
            return dice.Roll();
        }

        public void SetFaceChance(IDiceFace face, double newChance)
        {
            dice.SetFaceChance(face, newChance);
        }

        public void SetFaceSprite(IUniDiceFace face,Sprite sprite)
        {
            face.SetSprite(sprite);
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

    }
}