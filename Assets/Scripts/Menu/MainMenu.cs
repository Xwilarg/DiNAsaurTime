using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamedevGBG.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayMainScene()
        {
            SceneManager.LoadScene(1);
        }
    }
}
