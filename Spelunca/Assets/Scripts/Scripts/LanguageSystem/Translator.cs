using UnityEngine;
using System.Collections;
using TMPro;

namespace Language
{
    public class Translator : MonoBehaviour
    {
        public string textId;
        private TextMeshProUGUI textComponent => GetComponent<TextMeshProUGUI>();

        void Start()
        {
            if (!textComponent)
                Debug.LogError("Translator - textComponent is null.");
        }

        public void changeText(string text)
        {
            textComponent.text = text;
        }
    }
}