using DG.Tweening;
using UnityEngine;

namespace _Scripts.UI
{
    public class UIMover : MonoBehaviour
    {
        [SerializeField] private Transform _letterPoints;
        [SerializeField] private float _moveDuration;

        public void Move()
        {
            transform.DOMove(_letterPoints.position, _moveDuration);
        }
    }
}