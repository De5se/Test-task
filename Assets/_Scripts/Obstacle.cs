using DG.Tweening;
using UnityEngine;

namespace _Scripts
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Color materialColorOnLose;
        [SerializeField] private float colorChangeDuration = 1f;

        private Material _objectMaterial;

        private void Start()
        {
            _objectMaterial = GetComponent<MeshRenderer>().material;
        }

        private void OnEnable()
        {
            PlayerController.LoseEvent += ChangeMaterialColor;
        }

        private void OnDisable()
        {
            PlayerController.LoseEvent -= ChangeMaterialColor;
        }
        
        private void ChangeMaterialColor()
        {
            _objectMaterial.DOColor(materialColorOnLose, colorChangeDuration);
        }
    }
}