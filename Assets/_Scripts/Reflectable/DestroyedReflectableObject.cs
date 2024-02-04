using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.GameCamera;
using _Scripts.Sounds;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Reflectable
{
    public class DestroyedReflectableObject : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _rigidbodies;
        [SerializeField] private float _explosionForce;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _delayingDestruction;
        [SerializeField] private float _endScaleParts;
        [SerializeField] private float _partDestructionTime;
        [SerializeField] private MeshRenderer[] _meshRenderers;

        private WaitForSeconds _delay;
        private List<Coroutine> _partDestructionRoutines = new List<Coroutine>();
        [SerializeField] private List<Vector3> _defaultPosition = new List<Vector3>();
        [SerializeField] private List<Quaternion> _defaultRotation = new List<Quaternion>();

        public MeshRenderer[] MeshRenderers
        {
            get => _meshRenderers;
            set => _meshRenderers = value;
        }


        private void Start()
        {
            _delay = new WaitForSeconds(_delayingDestruction);
            for (int i = 0; i < _rigidbodies.Length; i++)
            {
                _defaultPosition.Add(_rigidbodies[i].transform.localPosition); 
                _defaultRotation.Add(_rigidbodies[i].transform.localRotation);
            }
        }

        public void Crash(Vector3 explosionPosition)
        {
            CameraEffects.ShakeCamera.Invoke();
            SoundManager.GlassCrashSound.Invoke();
            for (int i = 0; i < _rigidbodies.Length; i++)
            {
                _rigidbodies[i].gameObject.SetActive(true);
                _rigidbodies[i].isKinematic = false;
                _rigidbodies[i].AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
                Coroutine destr = StartCoroutine(PartDestruction(_rigidbodies[i].gameObject));
                _partDestructionRoutines.Add(destr);
            }
        }

        private IEnumerator PartDestruction(GameObject part)
        {
            yield return _delay;
            part.transform.DOScale(_endScaleParts, _partDestructionTime).onComplete = () =>
            {
                part.SetActive(false);
                part.transform.localScale = Vector3.one;
            };
        }

        public void RecoverObject()
        {
            for (int i = 0; i < _partDestructionRoutines.Count; i++)
            {
                StopCoroutine(_partDestructionRoutines[i]);
            }
            for (int i = 0; i < _rigidbodies.Length; i++)
            {
                _rigidbodies[i].gameObject.SetActive(false);
                _rigidbodies[i].isKinematic = true;
                _rigidbodies[i].velocity = Vector3.zero;
                _rigidbodies[i].transform.localPosition = _defaultPosition[i];
                _rigidbodies[i].transform.localScale = Vector3.one;
                _rigidbodies[i].transform.localRotation = _defaultRotation[i];
            }
        }
    }
}