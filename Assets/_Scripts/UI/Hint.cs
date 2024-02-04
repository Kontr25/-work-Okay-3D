using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class Hint : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private GameObject[] _hintPoints;
        [SerializeField] private Image _ball;
        [SerializeField] private float _flickDelayValue;

        private Coroutine _flickeringRoutine;
        private WaitForSeconds _flickDelay;
        private bool _isActive = false;

        [SerializeField] private GameObject _currentHint;

        public bool IsActive => _isActive;

        private void Start()
        {
            _flickDelay = new WaitForSeconds(_flickDelayValue);
        }

        public void ShowHint()
        {
            if (!_ball.gameObject.activeInHierarchy)
            {
                _ball.gameObject.SetActive(true);
            }
            _isActive = true;
            for (int i = 0; i < _hintPoints.Length; i++)
            {
                if (_hintPoints[i].activeInHierarchy)
                {
                    _currentHint = _hintPoints[i];
                }
            }

                _ball.transform.position = _mainCamera.WorldToScreenPoint(_currentHint.transform.position);

                _flickeringRoutine = StartCoroutine(StartFlickering());
            
        }

        public void StopFlickering()
        {
            if (_flickeringRoutine != null)
            {
                StopCoroutine(_flickeringRoutine); 
            }
            _ball.gameObject.SetActive(false);
        }

        private IEnumerator StartFlickering()
        {
            while (true)
            {
                _ball.DOFade(1, _flickDelayValue + 0.1f);
                yield return _flickDelay;
                _ball.DOFade(0, _flickDelayValue + 0.1f);
                yield return _flickDelay;
            }
        }
    }
}