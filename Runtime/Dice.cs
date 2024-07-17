using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PerikDiceRoller
{
    public class Dice: IDice
    {
        public Dictionary<IDiceFace, double> Faces { get; private set; }

        public Dice() 
        {
            Faces = new Dictionary<IDiceFace, double>();
        }

        public Dice(IDiceFace[] diceFaces) 
        {
            Faces = new Dictionary<IDiceFace, double>();

            foreach (var face in diceFaces) 
            { 
                Faces.Add(face, 100/diceFaces.Length);
            }
        }

        public Dice(Tuple<IDiceFace, double>[] tuples) 
        {
            Faces = new Dictionary<IDiceFace, double>();

            foreach (var tuple in tuples)
            {
                Faces.Add(tuple.Item1, tuple.Item2);
            }
        }

        public IDiceFace Roll() => DiceRoller.Roll(this);
        public void AddFace(IDiceFace face, double chance)
        {
            Faces.Add(face, 0); 
            SetFaceChance(face, chance);
        }

        public IDiceFace AddFace(int value, double chance)
        {
            IDiceFace diceFace = new DiceFace(value);
            Faces.Add(diceFace, 0);
                SetFaceChance(diceFace, chance);
            return diceFace;
        }

        public void RemoveFace(IDiceFace face) 
        {
            if (Faces.ContainsKey(face))
                Faces.Remove(face);
        }

        public void SetFaceValue(IDiceFace face, int value) 
            => face.SetValue(value);

        public void SetFaceChance(IDiceFace face, double newChance)
        {
            if (newChance < 0) newChance = 0;
            Faces[face] = newChance;
        }

        public void ResetChances()
        {
            foreach (var face in Faces.Keys)
            {
                Faces[face] = 50;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Dice {GetHashCode()}");
            int index = 0;
            foreach (var face in Faces) 
            {
                index++;
                sb.AppendLine($"Face {index}: Value - {face.Key.Value}, Chance - {face.Value}");    
            }

            return sb.ToString();
        }
    }
}