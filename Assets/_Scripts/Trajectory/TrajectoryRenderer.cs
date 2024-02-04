using System;
using _Scripts.Reflectable;
using UnityEngine;

namespace _Scripts.Trajectory
{
    public class TrajectoryRenderer : MonoBehaviour
    {
        [SerializeField] private int _reflections;
        [SerializeField] private float _maxLength;
        [SerializeField] private LineRenderer _invisibleLineRenderer;        
        [SerializeField] private LineRenderer _visibleLineRendererFirst;
        [SerializeField] private LineRenderer _visibleLineRendererSecond;
        [SerializeField] private GameObject _cancelingSprite;
        [SerializeField] private LayerMask _reflectedLayer;
        [SerializeField] private float _distanceDivider;

        private Ray _ray;
        private RaycastHit _hit;
        private Vector3 _direction;
        private bool _isRender = false;

        public LineRenderer CannonLineRenderer
        {
            get => _invisibleLineRenderer;
            set => _invisibleLineRenderer = value;
        }

        private void Update()
        {
            if (_isRender)
            {
                TrajectoryDraw();
            }
        }

        public void StartRenderTrajectory()
        {
            _invisibleLineRenderer.enabled = true;
            _visibleLineRendererFirst.enabled = true;
            _visibleLineRendererSecond.enabled = true;
            _isRender = true;
        }
        
        public void StopRenderTrajectory()
        {
            _isRender = false;
            _invisibleLineRenderer.enabled = false;
            _visibleLineRendererFirst.enabled = false;
            _visibleLineRendererSecond.enabled = false;
        }

        public void VisibleTrajectory(Vector3 endPosition)
        {
            if (_isRender)
            {
                _visibleLineRendererFirst.SetPosition(0, transform.position);
                _visibleLineRendererFirst.SetPosition(1, _invisibleLineRenderer.GetPosition(1));
                if (_invisibleLineRenderer.positionCount > 2)
                {
                    Vector3 direction = Vector3.Lerp(_visibleLineRendererFirst.GetPosition(1),_invisibleLineRenderer.GetPosition(2),_distanceDivider
                        / Vector3.Distance(_visibleLineRendererFirst.GetPosition(1),_invisibleLineRenderer.GetPosition(2)) );
                    _visibleLineRendererSecond.SetPosition(0, _visibleLineRendererFirst.GetPosition(1) );
                    _visibleLineRendererSecond.SetPosition(1, direction);
                }
            }
        }

        public void TrajectoryDraw()
        {
            _ray = new Ray(transform.position, transform.forward);

            _invisibleLineRenderer.positionCount = 1;
            _invisibleLineRenderer.SetPosition(0, transform.position);
            float remainingLength = _maxLength;

            for (int i = 0; i < _reflections; i++)
            {
                if (Physics.SphereCast(_ray.origin, .45f, _ray.direction, out _hit, remainingLength, _reflectedLayer))
                {
                    _invisibleLineRenderer.positionCount += 1;
                    _invisibleLineRenderer.SetPosition(_invisibleLineRenderer.positionCount - 1, _hit.point);
                    remainingLength -= Vector3.Distance(_ray.origin, _hit.point);
                    Vector3 reflectedDirection = Vector3.Reflect(_ray.direction.normalized, _hit.normal.normalized);
                    _ray = new Ray(_hit.point, reflectedDirection);

                    if (_hit.collider.TryGetComponent(out ReflectableObject reflectableObject) ||_hit.collider.TryGetComponent(out ReflectableWall wall))
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    _invisibleLineRenderer.positionCount += 1;
                    _invisibleLineRenderer.SetPosition(_invisibleLineRenderer.positionCount - 1, _ray.origin + _ray.direction * remainingLength);
                }
            }
        }

        public void CancelingShot(float distance)
        {
            _cancelingSprite.transform.localScale = new Vector3(1.6f - distance, 1.6f - distance, 1.6f - distance);
        }
    }
}