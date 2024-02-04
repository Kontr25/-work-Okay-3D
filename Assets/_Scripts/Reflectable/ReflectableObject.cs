using System.Collections;
using UnityEngine;

namespace _Scripts.Reflectable
{
    public class ReflectableObject : MonoBehaviour
    {
        [SerializeField] private DestroyedReflectableObject _destroyedObject;
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _mesh;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private float _appirianceDuration;

        private Coroutine _metallicRoutine;
        private Coroutine _smoothnessRoutine;
        private bool _isDestroyed = false;

        private void Start()
        {
            Obstacles.Instance.MaxNumberOfObject++;
            Obstacles.Instance.ReflectableObjects.Add(this);
            Appiriance();
        }

        private void OnEnable()
        {
            Obstacles.Instance.CurrentNumberOfObject++;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerBall playerBall) && !_isDestroyed && playerBall.ShotForce > 0)
            {
                Obstacles.Instance.CurrentNumberOfObject--;
                _isDestroyed = true;
                _mesh.SetActive(false);
                _collider.enabled = false;
                playerBall.RotateToNextPoint();
                _destroyedObject.Crash(playerBall.transform.position);
            }
            else if (other.TryGetComponent(out PlayerBall Ball) && !_isDestroyed && Ball.ShotForce == 0)
            {
                Ball.CancelShot();
            }
        }

        private void Appiriance()
        {
            if (_metallicRoutine != null)
            {
                StopCoroutine(_metallicRoutine);
            }
            if (_smoothnessRoutine != null)
            {
                StopCoroutine(_smoothnessRoutine);
            }
            _metallicRoutine = StartCoroutine(smoothMetallic(0, 0.6f, _appirianceDuration));
            _smoothnessRoutine = StartCoroutine(smoothSmoothness(0, 0.8f, _appirianceDuration));
        }

        public void RecoverObject()
        {
            if (_isDestroyed)
            {
                Appiriance();
                Obstacles.Instance.CurrentNumberOfObject++;
                _destroyedObject.RecoverObject();
                _isDestroyed = false;
                _mesh.SetActive(true);
                _collider.enabled = true;
            }
        }
        
        private IEnumerator smoothMetallic (float from, float to, float timer)
        {
            float t = 0.0f;
                 
            _meshRenderer.material.SetFloat("_Metallic", from);

            while (t < 1.0f) {
                t += Time.deltaTime * (1.0f / timer);
     
                _meshRenderer.material.SetFloat("_Metallic",  Mathf.Lerp (from, to, t));  

                yield return 0;
            }
        }
        
        private IEnumerator smoothSmoothness (float from, float to, float timer)
        {
            float t = 0.0f;
                 
            _meshRenderer.material.SetFloat("_Glossiness", from);

            while (t < 1.0f) {
                t += Time.deltaTime * (1.0f / timer);
     
                _meshRenderer.material.SetFloat("_Glossiness",  Mathf.Lerp (from, to, t));  

                yield return 0;
            }
        }
    }
}