using DG.Tweening;
using UnityEngine;

namespace _Scripts.Reflectable
{
    public class FadeWall : MonoBehaviour
    {
        [SerializeField] private GameObject _mesh;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private AudioSource _fufSound;
        [SerializeField] private float _appirianceDuration;
        [SerializeField] private Collider _collider;

        private bool _isVanish = false;
        private Vector3 _defaultScale;
        private Color _defaultColor;

        private void Start()
        {
            _defaultColor = _meshRenderer.material.color;
            _defaultScale = transform.localScale;
            Obstacles.Instance.MaxNumberOfObject++;
            Obstacles.Instance.FadeWalls.Add(this);
            Obstacles.Instance.CurrentNumberOfObject++;
            Apppiriance();
        }

        private void Apppiriance()
        {
            _meshRenderer.material.DOFade(.5f, _appirianceDuration);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerBall playerBall)&& !_isVanish)
            {
                _isVanish = true;
                Vanish();
            }
        }

        private void Vanish()
        {
            Obstacles.Instance.CurrentNumberOfObject--;
            _fufSound.Play();
            transform.DOScale(transform.localScale * 1.5f, .4f);
            _meshRenderer.material.DOFade(0, .4f).onComplete = () =>
            {
                _mesh.SetActive(false);
            };
        }

        public void RecoverObject()
        {
            if (_isVanish)
            {
                Obstacles.Instance.CurrentNumberOfObject++;
                _isVanish = false;
                transform.localScale = _defaultScale;
                _mesh.SetActive(true);
                Apppiriance();
            }
        }
    }
}