using GamedevGBG.Translation;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamedevGBG.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _board;

        private string Tr(string text)
        {
            return Translate.Instance.Tr(text);
        }

        public void Process(string key)
        {
            if (key == "PLAY")
            {
                SceneManager.LoadScene(1);
            }
            else if (key == "CREDITS")
            {
                _board.fontSize = .3f;
                _board.text =
                    $"{Tr("scientist and gamedesign")}: Paulo Gonçalves Teixeira\n" +
                    $"{Tr("3D artist")}: Jadith Nicole Bruzenak\n" +
                    $"{Tr("sound designer")}: kbrecordzz\n" +
                    $"{Tr("developer")}: Christian Chaux\n\n" +
                    $"{Tr("dutch translation")}: TheIndra\n" +
                    $"{Tr("spanish and german translation")}: Masaya-jkl";
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
            else if (key == "SECRET")
            {
                SceneManager.LoadScene("Secret");
            }
            else
            {
                throw new NotImplementedException($"Unknown key {key}");
            }
        }
    }
}
