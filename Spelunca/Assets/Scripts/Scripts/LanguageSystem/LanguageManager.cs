using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Language
{
    public class LanguageManager : MonoBehaviour
    {
        public TextAsset xmlFile;

        private Dictionary<string, Dictionary<string, string>> languages = new Dictionary<string, Dictionary<string, string>>();
        private Dictionary<string, string> dict;

        private List<Translator> translators;

        private void Awake()
        {
            XMLReader();
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelIsLoaded;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelIsLoaded;
        }

        private void OnLevelIsLoaded(Scene scene, LoadSceneMode mode)
        {
            GetTranslators();
            ApplyLanguage("EN");
        }

        public void ApplyLanguage(string lang)
        {
            foreach (Translator tr in translators)
            {
                string text = "TextError";

                if (!languages.ContainsKey(lang))
                    Debug.LogError("TranslationSystem - lang (" + lang + ") not found in the languages dictionary.");
                else if (!languages[lang].ContainsKey(tr.textId))
                    Debug.LogError("TranslationSystem - textId (" + tr.textId + ") not found in the text dictionary of the " + lang + " dictionary.");
                else
                    text = languages[lang][tr.textId];

                tr.changeText(text);
            }
        }

        private void GetTranslators()
        {
            translators = new List<Translator>();

            foreach (GameObject gameObject in SceneManager.GetActiveScene().GetRootGameObjects())
                translators.AddRange(gameObject.GetComponentsInChildren<Translator>());
        }

        private void XMLReader()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNodeList langList = doc.GetElementsByTagName("lang");

            foreach (XmlNode lang in langList)
            {
                XmlNodeList langContent = lang.ChildNodes;
                dict = new Dictionary<string, string>();

                foreach (XmlNode textNode in langContent)
                    dict.Add(textNode.Attributes[0].Value, textNode.InnerText);

                languages.Add(lang.Attributes[0].Value, dict);
            }
        }
    }
}