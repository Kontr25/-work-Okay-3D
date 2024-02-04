using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Reflectable
{
    public class MoverRatator : MonoBehaviour
    {
        [SerializeField] private bool _isMobile;
        [SerializeField] private bool _isRotating;
        [SerializeField] private float _moveX;
        [SerializeField] private float _moveZ;
        [SerializeField] private float _yRotationSpeed = 50;
        [SerializeField] private float _moveDurationValue;

        private Coroutine _moveRoutine;
        private WaitForSeconds _moveDelay;
        private Vector3 _defaultPosition;
        private Vector3 _targetPosition;

        private void Start()
        {
            _defaultPosition = transform.position;
            _targetPosition = new Vector3(_moveX, transform.position.y, _moveZ);
            _moveDelay = new WaitForSeconds(_moveDurationValue + 0.1f);
            if (_isMobile)
            {
                _moveRoutine = StartCoroutine(MoveRoutine());
            }
        }

        private void Update()
        {
            if (_isRotating)
            {
                transform.Rotate(0,_yRotationSpeed * Time.deltaTime, 0);
            }
        }

        private IEnumerator MoveRoutine()
        {
            while (true)
            {
                transform.DOMove(_targetPosition, _moveDurationValue);
                yield return _moveDelay;
                transform.DOMove(_defaultPosition, _moveDurationValue);
                yield return _moveDelay;
            }
        }
    }
}