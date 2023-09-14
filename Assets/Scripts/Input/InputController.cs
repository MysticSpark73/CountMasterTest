using UnityEngine;

namespace CountMasters.Input
{
    public class InputController : MonoBehaviour
    {
        private Vector2 _cursorPos;
        private bool _isTouchDown;

        private void Update()
        {
            GetCursor();
        }

        private void GetCursor()
        {
#if UNITY_EDITOR
            _isTouchDown = UnityEngine.Input.GetMouseButton(0);
            if (_isTouchDown) _cursorPos = UnityEngine.Input.mousePosition;

#elif UNITY_ANDROID

            //TODO: move this down
            _isTouchDown = UnityEngine.Input.touchCount > 0;
            if (_isTouchDown) _cursorPos = UnityEngine.Input.GetTouch(0).position;

#endif
            
            InputEvents.CursorMoved?.Invoke(_isTouchDown, _cursorPos);

        }
    }
}
