using TMPro;
using UnityEngine;

namespace _Scripts.Levels
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelNumberText;
        private int _number;

        public int LevelNumber
        {
            get => _number;
            set
            {
                _levelNumberText.text = value.ToString();
                _number = value;
            }
        }

        public void LoadLevel()
        {
            LevelLoader.LoadLevelAction.Invoke(_number);
        }
    }
}