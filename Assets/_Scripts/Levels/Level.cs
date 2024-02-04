using _Scripts;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private EnvironmentSetter _environmentSetter;
    [SerializeField] private FloorType _floorType;
    [SerializeField] private GroundColor groundColor;
    [SerializeField] private BackGroundType backGroundType;
    [SerializeField] private TrajectoryColor _trajectoryColor;

    private void Awake()
    {
        _environmentSetter.Collor = groundColor;
    }

    private void Start()
    {
        _environmentSetter.SetEnvironment(backGroundType, _floorType, groundColor, _trajectoryColor);
    }
}
