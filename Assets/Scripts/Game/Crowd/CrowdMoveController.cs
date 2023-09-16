using CountMasters.Core;
using CountMasters.Input;
using UnityEngine;

namespace CountMasters.Game.Crowd
{
    public class CrowdMoveController : IInitable
    {
        public bool IsMoving => isMoving;
        
        private readonly float _levelBoundaries = 3.5f;
        private readonly float _crowdSpeed = 3f;
        private Transform _selfTransform;

        private bool isMoving;

        public CrowdMoveController(Transform transform)
        {
            _selfTransform = transform;
        }

        public void Init(params object[] args) { }

        public void SetMoving(bool value) => isMoving = value;

        public void Move() => _selfTransform.position += Vector3.forward * _crowdSpeed * Time.deltaTime;

        public void CursorMoved(bool isTouchDown, Vector2 cursorPos)
        {
            if(!isTouchDown || !isMoving) return;
            _selfTransform.position = new Vector3(
                Mathf.Clamp(InputEvents.GetCursorPosClamped(cursorPos).x * 6 - _levelBoundaries, -_levelBoundaries, _levelBoundaries),
                _selfTransform.position.y,
                _selfTransform.position.z);
        }
    }
}