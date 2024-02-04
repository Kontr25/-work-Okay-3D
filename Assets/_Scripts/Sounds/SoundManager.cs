using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Sounds
{
    public class SoundManager : MonoBehaviour
    {
        public static Action GlassCrashSound;
        public static Action VictorySound;
        
        [SerializeField] private AudioSource[] _glassChashSounds;
        [SerializeField] private AudioSource _victorySound;

        private AudioSource _currentGlassCrashSound;

        private void OnEnable()
        {
            VictorySound += Victory;
            GlassCrashSound += GlassCrash;
        }
        
        private void OnDisable()
        {
            GlassCrashSound -= GlassCrash;
            VictorySound -= Victory;
        }

        private void GlassCrash()
        {
            _currentGlassCrashSound = _glassChashSounds[Random.Range(0, _glassChashSounds.Length)];
            _currentGlassCrashSound.Play();
        }

        private void Victory()
        {
            _victorySound.Play();
        }
    }
}