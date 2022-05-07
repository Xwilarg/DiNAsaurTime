using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamedevGBG.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void Process(string key)
        {
            if (key == "PLAY")
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                throw new NotImplementedException($"Unknown key {key}");
            }
        }
    }
}
