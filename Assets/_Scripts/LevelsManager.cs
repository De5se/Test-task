using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class LevelsManager : Singleton<LevelsManager>
    {

        public static void ReplayLevel()
        {
            var currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }
    }
}