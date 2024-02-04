using System;
using _Scripts.EnvironmentObject;
using _Scripts.Reflectable;
using _Scripts.Trajectory;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts.Player
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private PlayerBall _playerBall;
        [SerializeField] private TrajectoryRenderer _trajectoryRenderer;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private StretchView _stretchView;
        [SerializeField] private float _shotForce;
        [SerializeField] private GameObject[] _tutorials;
        [SerializeField] private Hint _hint;

        private Vector3 _shotDirection;
        private Ray inputRay;
        RaycastHit inpuHit;
        private bool _isCanShot = false;

        public Vector3 ShotDirection
        {
            get => _shotDirection;
            set => _shotDirection = value;
        }

        public bool IsCanShot
        {
            get => _isCanShot;
            set
            {
                _stretchView.RenderIs(false);
                _isCanShot = value;
            }
        }

        public void MouseDown()
        {
            if (Obstacles.Instance.CurrentNumberOfObject != Obstacles.Instance.MaxNumberOfObject)
            {
                Obstacles.Instance.RecoverAll();
            }

            if (_hint.IsActive)
            {
                _hint.StopFlickering();
            }

            for (int i = 0; i < _tutorials.Length; i++)
            {
                if (_tutorials[i].activeInHierarchy)
                {
                    _tutorials[i].SetActive(false);
                }
            }

            _playerBall.Trail.emitting = false;
            _playerBall.Trail.Clear();
            
            inputRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.SphereCast(inputRay, .5f, out inpuHit))
            {
                if (!inpuHit.collider.TryGetComponent(out Border border))
                {
                    Debug.Log("NotBorder");
                    return;
                }
                else
                {
                    Debug.Log("Border");
                    _stretchView.StartPosition = inpuHit.point;
                    _playerBall.OnStart(inpuHit.point);
                    _isCanShot = true;
                }
            }
        
        }

        private void FixedUpdate()
        {
            if (_isCanShot)
            {
                inputRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.SphereCast(inputRay, .5f, out inpuHit, 200, _targetLayer))
                {
                    if (inpuHit.collider.TryGetComponent(out Border ground) ||
                        inpuHit.collider.TryGetComponent(out ReflectableObject reflectableObject))
                    {
                        Vector3 endPosition = new Vector3(inpuHit.point.x, 1, inpuHit.point.z);
                        _shotDirection = endPosition - _playerBall.transform.position;
                        if (Vector3.Distance(endPosition, _playerBall.transform.position) > 1.6f)
                        {
                            _playerBall.CanShot = true;
                            _trajectoryRenderer.VisibleTrajectory(endPosition);
                            _playerBall.RotateToTarget(-_shotDirection);
                            _stretchView.RenderStretch(endPosition);
                        }
                        else
                        {
                            _playerBall.CanShot = false;
                            _trajectoryRenderer.CancelingShot(Vector3.Distance(endPosition,
                                _playerBall.transform.position));
                        }
                    }
                }
            }
        }

        public void MouseDrag()
        {
            
        }

        public void MouseUp()
        {
            _stretchView.RenderIs(false);
            if (_playerBall.CanShot)
            {
                _isCanShot = false;
                _playerBall.Shot(_shotDirection);
                _playerBall.ShotForce = _shotForce;
            }
            else
            {
                _playerBall.CancelShot();
            }
        }
    }
}