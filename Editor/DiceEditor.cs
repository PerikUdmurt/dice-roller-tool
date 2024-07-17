using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PerikDiceRoller.Editors
{
    [CustomEditor(typeof(MonoDice))]
    public class DiceEditor : Editor
    {
        private List<FaceContainer> _faceContainers = new List<FaceContainer>();
        private MonoDice _dice;
        private Sprite _sprite = null;
        private float _chance = 0f;
        private int _value = 0;

        public override void OnInspectorGUI()
        {
            _dice = (MonoDice)target;

            DrawDefaultInspector();
            ShowFaceCreator();
            ShowRollButton();
            DrawFaceContainers();
            UpdateDice();
        }

        private void ShowRollButton()
        {
            if (_dice.Faces.Count > 0)
            {
                if (GUILayout.Button("Roll The Dice"))
                {
                    IDiceFace face = _dice.Roll();
                    Debug.Log($"Result:{face.Value}");
                }
            }
        }

        private void ShowFaceCreator()
        {
            EditorGUILayout.LabelField("FaceCreator", EditorStyles.boldLabel);
            _sprite = EditorGUILayout.ObjectField(_sprite, typeof(Sprite), allowSceneObjects: false) as Sprite;
            _chance = EditorGUILayout.Slider("Chance", _chance, 1f, 100f);
            _value = EditorGUILayout.IntField("Value", _value);
            
            if (GUILayout.Button("Add Face"))
            {
                IUniDiceFace diceFace = _dice.AddFace(_value, _sprite, _chance);
                AddContainer(diceFace);
            }
        }

        private void AddContainer(IUniDiceFace face)
        {
            FaceContainer container = new FaceContainer(face ,face.Value, _dice.Faces[face], face.Sprite);
            _faceContainers.Add(container);
        }

        private void RemoveContainer(FaceContainer container)
        {
            _faceContainers.Remove(container);
            _dice.RemoveFace(container.face);
        }

        private void UpdateDice()
        {
            foreach (FaceContainer container in _faceContainers)
            {
                _dice.SetFaceChance(container.face ,container.chance);
                _dice.SetFaceValue(container.face, container.value);
                _dice.SetFaceSprite(container.face, container.sprite);
            }
        }

        private void ShowDiceinfo()
        {
            EditorGUILayout.LabelField("DiceInfo", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(_dice.dice.ToString(), EditorStyles.wordWrappedMiniLabel);
        }

        private void DrawFaceContainers()
        {
            if (_faceContainers.Count > 0)
            {
                EditorGUILayout.LabelField("Faces", EditorStyles.boldLabel);
                foreach (var container in _faceContainers)
                {
                    DrawContainer(container);
                }
            }
        }

        private void DrawContainer(FaceContainer container)
        {
            container.totalDrops = container.face.TotalDrops;
            Sprite previousSprite = container.sprite;
            int previousValue = container.value;
            double previousChance = container.chance;
            string index = container.name;
            bool foldout = container.isFoldout;
            double result = Math.Round((container.chance / _faceContainers.Sum(x => x.chance)) * 100, 3);

            container.isFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, index, EditorStyles.foldoutPreDrop);
            if (foldout)
            {
                container.name = $"Face {_faceContainers.IndexOf(container) + 1}";
                container.sprite = EditorGUILayout.ObjectField("Sprite", previousSprite, typeof(Sprite), allowSceneObjects: false) as Sprite;
                container.value = EditorGUILayout.IntField("Value", previousValue);
                EditorGUILayout.IntField("TotalDrops", container.face.TotalDrops);
                container.chance = EditorGUILayout.Slider("Chance",(float)previousChance, 0f, 100f);
                EditorGUILayout.LabelField(result.ToString());
                if (GUILayout.Button("X"))
                    RemoveContainer(container);
            }
            if (!foldout)
            {
                container.name = $"Face {_faceContainers.IndexOf(container) + 1} " +
                    $"Value: {container.value} " +
                    $"Chance: {result} " +
                    $"TotalDrops: {container.totalDrops}";
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        public class FaceContainer
        {
            public string name;
            public IUniDiceFace face;
            public int value;
            public int totalDrops;
            public Sprite sprite;
            public double chance;
            public bool isFoldout;
            public bool isDeleted;

            public FaceContainer(IUniDiceFace face, int value, double chance, Sprite sprite = null)
            {
                this.face = face;
                this.value = value;
                totalDrops = face.TotalDrops;
                this.sprite = sprite;
                this.chance = chance;
                isFoldout = false;
                isDeleted = false;
                name = "Empty";
            }
        }
    }
}