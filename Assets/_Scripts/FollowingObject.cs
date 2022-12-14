using NaughtyAttributes;
using UnityEngine;

namespace _Scripts
{
    [RequireComponent(typeof(Camera))]
    public class FollowingObject : MonoBehaviour
    {
        [SerializeField] private Transform objectToFollow;
        [SerializeField] private bool useSceneOffset;
        [HideIf(nameof(useSceneOffset))]
        [SerializeField] private Vector3 offset;
        [SerializeField] private float smoothTime = 0.5f;
        [Foldout("Constraints")]
        [SerializeField] private bool freezeXPosition;
        [Foldout("Constraints")]
        [SerializeField] private bool freezeYPosition;
        [Foldout("Constraints")]
        [SerializeField] private bool freezeZPosition;
        private Vector3 _velocity;
        private Vector3 _targetPosition;

        private void Start()
        {
            if (useSceneOffset)
            {
                UpdateOffset();
            }
        }

        private void LateUpdate()
        {
            Follow();
        }

        private void Follow()
        {
            var targetPosition = objectToFollow.position + offset;
            if  (freezeXPosition) {targetPosition.x = transform.position.x;}
            if  (freezeYPosition) {targetPosition.y = transform.position.y;}
            if  (freezeZPosition) {targetPosition.z = transform.position.z;}

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        }
        
        private void UpdateOffset()
        {
            offset = transform.position - objectToFollow.position;
        }
    }
}