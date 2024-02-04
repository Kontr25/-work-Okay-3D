using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace _Scripts.Reflectable
{
    public class Obstacles : MMSingleton<Obstacles>
    {
        [SerializeField] private int _currentNumberOfObject = 0;
        [SerializeField] private int _maxNumberOfObject = 0;
        [SerializeField] private List<ReflectableObject> _reflectableObjects = new List<ReflectableObject>();
        [SerializeField] private List<FadeWall> _fadeWalls = new List<FadeWall>();
        [SerializeField] private LevelLoader _levelLoader;
        [SerializeField] private SettingsMenu _settingsMenu;

        public int CurrentNumberOfObject
        {
            get => _currentNumberOfObject;
            set => _currentNumberOfObject = value;
        }

        public int MaxNumberOfObject
        {
            get => _maxNumberOfObject;
            set => _maxNumberOfObject = value;
        }

        public List<ReflectableObject> ReflectableObjects
        {
            get => _reflectableObjects;
            set => _reflectableObjects = value;
        }

        public List<FadeWall> FadeWalls
        {
            get => _fadeWalls;
            set => _fadeWalls = value;
        }

        public void RecoverAll()
        {
            FinishAction.Finish.Invoke(FinishAction.FinishType.Lose);
            _settingsMenu.Attempt();
            for (int i = 0; i < _reflectableObjects.Count; i++)
            {
                _reflectableObjects[i].RecoverObject();
            }

            for (int i = 0; i < _fadeWalls.Count; i++)
            {
                _fadeWalls[i].RecoverObject();
            }
        }
    }
}