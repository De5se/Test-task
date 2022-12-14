using UnityEngine;

namespace _Scripts.UI
{
    [RequireComponent(typeof(Animator))]
    public class TutorialWindow : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int CloseTrigger = Animator.StringToHash("Close");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _animator.SetTrigger(CloseTrigger);
            }
        }
        
        private void DisableObject()
        {
            gameObject.SetActive(false);
        }
    }
}