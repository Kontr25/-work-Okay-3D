using DG.Tweening;
using TMPro;
using UnityEngine;

public class CurrentLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentLevelText;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private Transform[] _moveTargetPoints;
    private void Start()
    {
        Invoke(nameof(Move), .5f);
    }

    private void Move()
    {
        _currentLevelText.text = (_levelLoader.CurrentLevel + 1).ToString();
        
        transform.DOMove(_moveTargetPoints[0].position, .4f).onComplete = () =>
        {
            transform.DOMove(_moveTargetPoints[1].position, 1f).onComplete = () =>
            {
                transform.DOMove(_moveTargetPoints[2].position, .4f);
            };
        };
    }
}
