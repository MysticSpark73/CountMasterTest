using CountMasters.Core;
using CountMasters.Input;
using UnityEngine;

namespace CountMasters.Game.Crowd
{
    public class CrowdMoveController : IInitable
    {
        //TODO: Add CameraFollow script
        //TODO: probably move to LevelController or elsewhere
        private readonly float _levelBoundaries = 1.5f;
        private Transform _selfTransform;

        public CrowdMoveController(Transform transform)
        {
            _selfTransform = transform;
        }
        
        public void Init()
        {
            
        }

        public void CursorMoved(bool isTouchDown, Vector2 cursorPos)
        {
            if(!isTouchDown) return;
            _selfTransform.position = new Vector3(
                Mathf.Clamp(InputEvents.GetCursorPosClamped(cursorPos).x * 3 - _levelBoundaries, -_levelBoundaries, _levelBoundaries),
                _selfTransform.position.y,
                _selfTransform.position.z);
        }
    }
}