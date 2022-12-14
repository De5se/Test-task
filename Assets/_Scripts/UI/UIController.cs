using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;
        [Space] [SerializeField] private Button winButton;
        [SerializeField] private Button loseButton;

        private void Start()
        {
            winButton.onClick.RemoveAllListeners();
            winButton.onClick.AddListener(LevelsManager.ReplayLevel);
            loseButton.onClick.RemoveAllListeners();
            loseButton.onClick.AddListener(LevelsManager.ReplayLevel);
        }

        private void OnEnable()
        {
            PlayerController.WinEvent += EnableWinPanel;
            PlayerController.LoseEvent += EnableLosePanel;
        }
        
        private void OnDisable()
        {
            PlayerController.WinEvent -= EnableWinPanel;
            PlayerController.LoseEvent -= EnableLosePanel;
        }
        
        private void EnableWinPanel()
        {
            winPanel.SetActive(true);
        }
        
        private void EnableLosePanel()
        {
            losePanel.SetActive(true);
        }
    }
}
