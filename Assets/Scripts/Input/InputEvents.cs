using System;
using UnityEngine;

namespace CountMasters.Input
{
    public static class InputEvents
    {
        public static Action<bool, Vector2> CursorMoved;

        public static Vector2 GetCursorPosClamped(Vector2 cursorPosUnclamped)
        {
            return new Vector2(cursorPosUnclamped.x / Screen.width, cursorPosUnclamped.y / Screen.height);
        }
    }
}
