using UnityEngine;

namespace CountMasters.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offsetPosition;
        [SerializeField] private Vector3 _offsetRotation;
        [SerializeField] [Range(0, 0.5f)] private float _smoothTime;

        private void Awake()
        {
            transform.localRotation = Quaternion.Euler(_offsetRotation);
        }

        private void LateUpdate()
        {
            Vector3 velocity = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, _target.position + _offsetPosition, ref velocity, _smoothTime);
        }
    }
}