using GamedevGBG.Translation;
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
            else if (key == "ENGLISH")
            {
                Translate.Instance.CurrentLanguage = "english";
            }
            else if (key == "FRENCH")
            {
                Translate.Instance.CurrentLanguage = "french";
            }
            else if (key == "SPANISH")
            {
                Translate.Instance.CurrentLanguage = "spanish";
            }
            else if (key == "DUTCH")
            {
                Translate.Instance.CurrentLanguage = "dutch";
            }
            else if (key == "GERMAN")
            {
                Translate.Instance.CurrentLanguage = "german";
            }
            else
            {
                throw new NotImplementedException($"Unknown key {key}");
            }
        }
    }
}
