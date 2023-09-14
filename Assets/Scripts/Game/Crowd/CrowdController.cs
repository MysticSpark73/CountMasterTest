using CountMasters.Input;
using UnityEngine;

namespace CountMasters.Game.Crowd
{
    public class CrowdController : MonoBehaviour
    {
        //TODO: probably move to LevelController or elsewhere
        private readonly float levelBoundaries = 1.5f;
        //reference to pooler

        private void Awake()
        {
            InputEvents.CursorMoved += OnCursorMoved;
        }

        private void OnApplicationQuit()
        {
            InputEvents.CursorMoved -= OnCursorMoved;
        }

        private void OnCursorMoved(bool isTouchDown, Vector2 cursorPos)
        {
            Debug.Log($"[OnCursorMoved] {isTouchDown}, {cursorPos}");
            if(!isTouchDown) return;
            transform.position = new Vector3(
                Mathf.Clamp(InputEvents.GetCursorPosClamped(cursorPos).x * 3 - levelBoundaries, -levelBoundaries, levelBoundaries),
                transform.position.y,
                transform.position.z);
        }
    }
}