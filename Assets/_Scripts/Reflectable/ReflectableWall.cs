using System;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Reflectable
{
    public class ReflectableWall : MonoBehaviour
    {
        [SerializeField] private Transform _meshTransform;
        [SerializeField] private AudioSource _wallSound;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private float _apperianceDuration;

        private void Start()
        {
            Apperiance();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerBall playerBall))
            {
                playerBall.RotateToNextPoint();
                Punch();
            }
        }

        public void Apperiance()
        {
            _meshRenderer.material.DOFade(1, _apperianceDuration);
        }
        private void Punch()
        {
            _wallSound.Play();
            _meshTransform.DOScale(1.5f, .2f).onComplete = () =>
            {
                _meshTransform.DOScale(1f, .2f).onComplete = () =>
                {
                    _meshTransform.localScale = Vector3.one;
                };
            };
        }
    }
}