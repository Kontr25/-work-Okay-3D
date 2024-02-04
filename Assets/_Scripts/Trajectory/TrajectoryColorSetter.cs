using System;
using UnityEngine;

namespace _Scripts.Trajectory
{
    public enum TrajectoryColor
    {
        white,
        black
    }
    public class TrajectoryColorSetter : MonoBehaviour
    {
        public static Action<TrajectoryColor> SetTrajectoryColor;
        
        [SerializeField] private LineRenderer _visibleLineRendererFirst;
        [SerializeField] private LineRenderer _visibleLineRendererSecond;
        [SerializeField] private Color _white;
        [SerializeField] private Color _black;
        [SerializeField] private Color _whiteEndColor;
        [SerializeField] private Color _blackEndColor;
        
        private void Awake()
        {
            SetTrajectoryColor += ChangeColor;
        }

        private void OnDestroy()
        {
            SetTrajectoryColor -= ChangeColor;
        }
        
        private void ChangeColor(TrajectoryColor color)
        {
            switch (color)
            {
                case TrajectoryColor.white:
                    _visibleLineRendererFirst.startColor = _white;
                    _visibleLineRendererFirst.endColor = _white;
                    _visibleLineRendererSecond.startColor = _white;
                    _visibleLineRendererSecond.endColor = _whiteEndColor;
                    break;
                case TrajectoryColor.black:
                    _visibleLineRendererFirst.startColor = _black;
                    _visibleLineRendererFirst.endColor = _black;
                    _visibleLineRendererSecond.startColor = _black;
                    _visibleLineRendererSecond.endColor = _blackEndColor;
                    break;
            }
        }
    }
}