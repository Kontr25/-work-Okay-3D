using System;
using Cinemachine;
using UnityEngine;

namespace _Scripts.GameCamera
{
    public class CameraEffects : MonoBehaviour
    {
        public static Action ShakeCamera;
        
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private float _intensity;
        [SerializeField] private float _time;
        private float _shakeTimer;

        private void OnEnable()
        {
            ShakeCamera += Shake;
        }

        private void OnDisable()
        {
            ShakeCamera -= Shake;
        }

        public void Shake()
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _intensity;
            _shakeTimer = _time;
        }

        private void FixedUpdate()
        {
            if (_shakeTimer > 0)
            {
                _shakeTimer -= Time.deltaTime;
                if (_shakeTimer <= 0f)
                {
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                }
            }
        }
    }
}