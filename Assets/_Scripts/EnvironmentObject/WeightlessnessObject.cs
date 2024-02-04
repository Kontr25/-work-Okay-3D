using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.EnvironmentObject
{
    public class WeightlessnessObject : MonoBehaviour
    {
        [SerializeField] private float _speedRotation;
        [SerializeField] private Rigidbody _rigidbody;
        private float _xAxis;
        private float _yAxis;
        private float _zAxis;
        
        private void Start()
        {
            _xAxis = Random.Range(-1,1);
            _yAxis = Random.Range(-1,1);
            _zAxis = Random.Range(-1,1);
            
            StartRotate();
        }

        private void StartRotate()
        {
            _rigidbody.angularVelocity = new Vector3(_xAxis, _yAxis, _zAxis) * _speedRotation;
        }
    }
}