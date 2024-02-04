using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Tutorial
{
    public class TutorialHandsMover : MonoBehaviour
    {
        [SerializeField] private Transform _ball;
        [SerializeField] private Image _hand;
        [SerializeField] private Transform _movePoint;
        [SerializeField] private Image[] _tutorialElements;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Transform[] _destroyObject;
        [SerializeField] private Image[] _destroyedImage;

        private void Start()
        {
            StartCoroutine(Move());
            for (int i = 0; i < _destroyObject.Length; i++)
            {
                _destroyedImage[i].transform.position = _mainCamera.WorldToScreenPoint(_destroyObject[i].position);
            }
        }

        private IEnumerator Move()
        {
            while (true)
            {
                _hand.transform.position = _ball.position;
                _hand.DOFade(1, .5f);
                yield return new WaitForSeconds(.3f);
                for (int i = 0; i < _tutorialElements.Length; i++)
                {
                    _tutorialElements[i].DOFade(1, .5f);
                }
                yield return new WaitForSeconds(.2f);
                _hand.transform.DOMove(_movePoint.position, 2f);
                yield return new WaitForSeconds(3f);
                _hand.DOFade(0, .5f);
                for (int i = 0; i < _tutorialElements.Length; i++)
                {
                    _tutorialElements[i].DOFade(0, .5f);
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}