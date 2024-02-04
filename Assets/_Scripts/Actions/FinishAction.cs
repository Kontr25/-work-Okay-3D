using System;
using System.Collections.Generic;
using UnityEngine;

public class FinishAction : MonoBehaviour
{
    public static Action<FinishType> Finish;
    public static bool IsGameOver = false;
    [SerializeField] private List<GameObject> _finishableObjects;
    [SerializeField] private ParticleSystem[] _confetti;

    private float _sessionTime = 0f;

    private void Start()
    {
        Finish += Activate;
    }

    private void OnDestroy()
    {
        Finish -= Activate;
    }

    private void Update()
    {
        _sessionTime += Time.deltaTime;
    }

    public void Activate(FinishType finishType = FinishType.None)
    {
        IsGameOver = true;
        if (_finishableObjects.Count > 0)
        {
            switch (finishType)
            {
                case FinishType.Win:

                    foreach (var obj in _finishableObjects)
                    {
                        if (obj.TryGetComponent(out IFinishable finishable))
                            finishable.StartActionOnWin();
                    }

                    for (int i = 0; i < _confetti.Length; i++)
                    {
                        _confetti[i].Play();
                    }

                    Debug.Log("GAINSTANCE ====> Win event");
                    break;

                case FinishType.Lose:

                    Debug.Log("GAINSTANCE ====> Lose event");
                    break;

                default:
                    break;
            }
        }
    }

    public enum FinishType
    {
        None,
        Win,
        Lose
    }
}