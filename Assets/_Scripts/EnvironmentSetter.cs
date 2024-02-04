using UnityEngine;

namespace _Scripts
{
    public enum TrajectoryColor
    {
        None,
        White,
        Black
    }
    public enum GroundColor
    {
        None,
        White,
        Gray
    }
    public enum BackGroundType
    {
        None,
        Green,
        White,
        Pink,
        LightBlue,
        Violet,
    }

    public enum FloorType
    {
        None,
        Violet,
        Blue,
        Pink,
        Yellow,
        LightViolet,
        Red,
        Black,
        White,
        
    }
    public class EnvironmentSetter : MonoBehaviour
    {
        public static EnvironmentSetter Instance;
        
        private GroundColor _groundColor;
        [SerializeField] private GameObject[] _ground;
        [SerializeField] private GameObject[] _backgrounds;
        [SerializeField] private GameObject[] _floors;
        [SerializeField] private LineRenderer _firstTrajectoryLine;
        [SerializeField] private LineRenderer _secondTrajectoryLine;
        [SerializeField] private Color _white;
        [SerializeField] private Color _black;
        [SerializeField] private Color _endWhite;
        [SerializeField] private Color _endBlack;
        [SerializeField] private MeshRenderer[] _wallsMeshes;
        [SerializeField] private Material[] _wallsMaterials;
        

        public GroundColor Collor
        {
            get => _groundColor;
            set => _groundColor = value;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                transform.SetParent(null);
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetEnvironment(BackGroundType backGroundType, FloorType floorType, GroundColor groundCollor, TrajectoryColor trajectoryColor)
        {
            switch (trajectoryColor)
            {
                case TrajectoryColor.White:
                    SetTrajectoryColor(_white, _endWhite);
                    break;
                case TrajectoryColor.Black:
                    SetTrajectoryColor(_black, _endBlack);
                    break;
            }

            switch (groundCollor)
            {
                case GroundColor.White:
                    _ground[0].SetActive(true);
                    break;
                case GroundColor.Gray:
                    _ground[1].SetActive(true);
                    break;
            }
            switch (backGroundType)
            {
                case BackGroundType.Green:
                    _backgrounds[0].SetActive(true);
                    break;
                case BackGroundType.White:
                    _backgrounds[1].SetActive(true);
                    break;
                case BackGroundType.Pink:
                    _backgrounds[2].SetActive(true);
                    break;
                case BackGroundType.LightBlue:
                    _backgrounds[3].SetActive(true);
                    break;
                case BackGroundType.Violet:
                    _backgrounds[4].SetActive(true);
                    break;
            }

            switch (floorType)
            {
                case FloorType.Violet:
                    _floors[0].SetActive(true);
                    SetWallsMeshes(_wallsMaterials[0]);
                    break;
                case FloorType.Blue:
                    _floors[1].SetActive(true);
                    SetWallsMeshes(_wallsMaterials[1]);
                    break;
                case FloorType.Pink:
                    _floors[2].SetActive(true);
                    SetWallsMeshes(_wallsMaterials[2]);
                    break;
                case FloorType.Yellow:
                    _floors[3].SetActive(true);
                    SetWallsMeshes(_wallsMaterials[3]);
                    break;
                case FloorType.LightViolet:
                    _floors[4].SetActive(true);
                    SetWallsMeshes(_wallsMaterials[4]);
                    break;
                case FloorType.Red:
                    _floors[5].SetActive(true);
                    SetWallsMeshes(_wallsMaterials[5]);
                    break;
                case FloorType.Black:
                    _floors[6].SetActive(true);
                    SetWallsMeshes(_wallsMaterials[6]);
                    break;
                case FloorType.White:
                    _floors[7].SetActive(true);
                    SetWallsMeshes(_wallsMaterials[7]);
                    break;
            }
        }

        private void SetWallsMeshes(Material material)
        {
            for (int i = 0; i < _wallsMeshes.Length; i++)
            {
                if (_wallsMeshes[i].gameObject.activeInHierarchy)
                {
                    _wallsMeshes[i].material = material;
                }
            }
        }

        private void SetTrajectoryColor(Color startColor, Color endColor)
        {
            _firstTrajectoryLine.startColor = startColor;
            _firstTrajectoryLine.endColor = startColor;
            _secondTrajectoryLine.startColor = startColor;
            _secondTrajectoryLine.endColor = endColor;
        }
    }
}