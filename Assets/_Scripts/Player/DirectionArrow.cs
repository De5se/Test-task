using System;
using UnityEngine;

namespace _Scripts.Player
{
    public class DirectionArrow : MonoBehaviour
    {
        [SerializeField] private Transform shaft;
        [SerializeField] private Transform arrowhead;

        private Vector3 _targetPoint;

        private Vector3 _targetShaftScale;
        private Vector3 _targetShaftPosition;
        private Vector3 _targetArrowheadPosition;
        private float _arrowheadOffset;

        private const float RotationOffset = 90f;

        private void Start()
        {
            _targetShaftScale = shaft.localScale;
            _targetShaftPosition = shaft.localPosition;
            _arrowheadOffset = _targetShaftScale.x - arrowhead.lossyScale.x;
        }

        public void Aim(Vector3 targetPoint)
        {
            _targetPoint = targetPoint;
            var size = Vector3.Distance(transform.position, targetPoint);
            UpdateArrowSize(size);
            RotateArrow();
        }

        private void UpdateArrowSize(float size)
        {
            _targetShaftScale.x = size;
            _targetShaftPosition.x = size / 2;

            shaft.localScale = _targetShaftScale;
            shaft.localPosition = _targetShaftPosition;
            
            _targetArrowheadPosition.x = size - _arrowheadOffset;
            arrowhead.localPosition = _targetArrowheadPosition;
        }

        private void RotateArrow()
        {
            var direction = _targetPoint - transform.position;
            var angle = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + RotationOffset;
            transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
