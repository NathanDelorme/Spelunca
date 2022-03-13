using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

namespace Language
{
    public class LevelTranslator : MonoBehaviour
    {
        public string textId;
        private TextMeshProUGUI textComponent => GetComponent<TextMeshProUGUI>();

        void Start()
        {
            if (!textComponent)
                Debug.LogError("Translator - textComponent is null.");
        }

        public void changeText(string translatedText)
        {
            int levelNb = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5));
            textComponent.SetText(translatedText + " " + levelNb);
        }
    }
}