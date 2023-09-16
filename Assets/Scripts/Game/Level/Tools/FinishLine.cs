using UnityEngine;

namespace CountMasters.Game.Level.Tools
{
    public class FinishLine : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            LevelEvents.LevelFinish?.Invoke();
        }
    }
}