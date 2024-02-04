using _Scripts.Reflectable;
using UnityEngine;

namespace _Scripts.EnvironmentObject
{
    public class Border : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out PlayerBall playerBall))
            {
                if (Obstacles.Instance.CurrentNumberOfObject > 0)
                {
                    Obstacles.Instance.RecoverAll();
                }
                else
                {
                    FinishAction.Finish.Invoke(FinishAction.FinishType.Win);
                }
            }
        }
    }
}