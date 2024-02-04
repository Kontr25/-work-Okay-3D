using System.Collections.Generic;
using _Scripts.Player;
using _Scripts.Trajectory;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletRigidbody;
    [SerializeField] private TrajectoryRenderer _trajectoryRenderer;
    [SerializeField] private List<Vector3> _reflectPoint = new List<Vector3>();
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GameObject _cancelingSprite;
    [SerializeField] private ParticleSystem _sparks;
    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private InputManager _inputManager;
    private const float _rayPositionY = 1;
    private int _currentPointID = 2;
    private bool _canShot = false;
    private float _shotForce;

    [SerializeField] private Transform[] _linePoints;

    private void FixedUpdate()
    {
        _bulletRigidbody.velocity = transform.forward * _shotForce;
    }

    public bool CanShot
    {
        get => _canShot;
        set
        {
            if (value)
            {
                _cancelingSprite.SetActive(false);
                _trajectoryRenderer.StartRenderTrajectory();
            }
            else
            {
                _cancelingSprite.SetActive(true);
                _trajectoryRenderer.StopRenderTrajectory();
            }
            _canShot = value;
        }
    }

    public float ShotForce
    {
        get => _shotForce;
        set => _shotForce = value;
    }

    public TrailRenderer Trail
    {
        get => _trail;
        set => _trail = value;
    }

    public void OnStart(Vector3 tapPosition)
    {
        _currentPointID = 2;
        _shotForce = 0;
        transform.position = new Vector3(tapPosition.x, _rayPositionY, tapPosition.z);
        gameObject.SetActive(true);
        _trajectoryRenderer.StartRenderTrajectory();
    }

    public void Shot(Vector3 shotDirection)
    {
        _trail.emitting = true;
        _trajectoryRenderer.StopRenderTrajectory();
        AddPoints();
    }
        
    private void AddPoints()
    {
        _reflectPoint.Clear();
        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            _reflectPoint.Add(_lineRenderer.GetPosition(i));
        }
    }

    public void RotateToNextPoint()
    {
        _sparks.transform.position = _reflectPoint[_currentPointID - 1];
        _sparks.Play();
        transform.LookAt(_reflectPoint[_currentPointID]);
        _currentPointID++;
    }

    public void RotateToTarget(Vector3 shotDirection)
    {
        transform.rotation = Quaternion.LookRotation(shotDirection);
    }

    public void CancelShot()
    {
        gameObject.SetActive(false);
        _trajectoryRenderer.StopRenderTrajectory();
        _inputManager.IsCanShot = false;
    }
}
