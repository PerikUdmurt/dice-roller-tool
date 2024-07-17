using System;
using System.Collections.Generic;
using System.Linq;

namespace PerikDiceRoller
{
    public static class DiceRoller
    {
        private static Random randomizer = new Random();

        public static IDiceFace Roll(IDice dice)
        {
            Dictionary<Tuple<double, double>, IDiceFace> faces = new Dictionary<Tuple<double, double>, IDiceFace>();
            faces.SetRangeForDiceFaces(dice);

            double sum = dice.Faces.Values.Sum();
            double totalValue = randomizer.NextDouble() * sum;
            IDiceFace result = faces.GetDiceFaceByValue(totalValue);
            result.AddDrop();
            return result;
        }

        public static int Roll(int maxFaces) => randomizer.Next(maxFaces) * 100;

        private static void SetRangeForDiceFaces(this Dictionary<Tuple<double, double>, IDiceFace> faces, IDice dice)
        {
            double currentLowValue = 0;

            foreach (var face in dice.Faces)
            {
                double highValue = currentLowValue + face.Value;

                faces.Add(new Tuple<double, double>(currentLowValue, highValue), face.Key);

                currentLowValue = highValue;
            }
        }

        private static IDiceFace GetDiceFaceByValue(this Dictionary<Tuple<double, double>, IDiceFace> faces, double value)
        {
            foreach (var pair in faces)
            {
                if (pair.Key.Item1 < value && value <= pair.Key.Item2)
                {
                    return pair.Value;
                }
            }
            return null;
        }
    }
}