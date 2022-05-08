using GamedevGBG.Prop;
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

        [SerializeField]
        private Machine _pcr;

        private string Tr(string text)
        {
            return Translate.Instance.Tr(text);
        }

        private bool _isListeningToUpdates = false;

        private void DisplayCredits()
        {
            _board.text =
                $"{Tr("scientist and gamedesign")}: Paulo Gonçalves Teixeira\n" +
                $"{Tr("3D artist")}: Jadith Nicole Bruzenak\n" +
                $"{Tr("sound designer")}: kbrecordzz\n" +
                $"{Tr("developer")}: Christian Chaux\n\n" +
                $"{Tr("dutch translation")}: TheIndra\n" +
                $"{Tr("spanish and german translation")}: Masaya-jkl";
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
                DisplayCredits();
                _pcr.SetText(Tr("thank you"));
                if (!_isListeningToUpdates)
                {
                    _board.GetComponent<TMP_TextTranslate>().OnTextUpdate += (sender, e) =>
                    {
                        DisplayCredits();
                    };
                    _isListeningToUpdates = true;
                }
            }
            else if (key == "ENGLISH")
            {
                Translate.Instance.CurrentLanguage = "english";
                _pcr.SetText(Tr("customText"));
            }
            else if (key == "FRENCH")
            {
                Translate.Instance.CurrentLanguage = "french";
                _pcr.SetText(Tr("customText"));
            }
            else if (key == "SPANISH")
            {
                Translate.Instance.CurrentLanguage = "spanish";
                _pcr.SetText(Tr("customText"));
            }
            else if (key == "DUTCH")
            {
                Translate.Instance.CurrentLanguage = "dutch";
                _pcr.SetText(Tr("customText"));
            }
            else if (key == "GERMAN")
            {
                Translate.Instance.CurrentLanguage = "german";
                _pcr.SetText(Tr("customText"));
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
