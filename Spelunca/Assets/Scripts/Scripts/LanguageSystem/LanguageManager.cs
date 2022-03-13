using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using static StatsLevelTranslator;

namespace Language
{
    public class LanguageManager : MonoBehaviour
    {
        public TextAsset xmlFile;

        private Dictionary<string, Dictionary<string, string>> languages = new Dictionary<string, Dictionary<string, string>>();
        private Dictionary<string, string> dict;

        private List<Translator> translators;
        private List<StatsLevelTranslator> statsTranslator;
        private List<LevelTranslator> levelsTranslator;
        private string lastLanguage = "EN";


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
            GetStatsTranslators();
            GetLevelsTranslators();
            ApplyLanguage("EN");
        }

        public void ApplyLanguage(string lang)
        {
            if (lang == "None")
                lang = lastLanguage;

            lastLanguage = lang;

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

            foreach (StatsLevelTranslator tr in statsTranslator)
            {
                List<string> texts = new List<string>();

                foreach (Stats stat in tr.statsToDisplay)
                {
                    string textStat = "Error";

                    switch(stat)
                    {
                        case Stats.LEVEL_DEATHS:
                            textStat = languages[lang]["LEVEL_DEATHS"];
                            break;
                        case Stats.LEVEL_JUMP:
                            textStat = languages[lang]["LEVEL_JUMP"];
                            break;
                        case Stats.LEVEL_FULLTIME:
                            textStat = languages[lang]["LEVEL_FULLTIME"];
                            break;
                        case Stats.LEVEL_BESTTIME:
                            textStat = languages[lang]["LEVEL_BESTTIME"];
                            break;
                        case Stats.ALL_DEATHS:
                            textStat = languages[lang]["ALL_DEATHS"];
                            break;
                        case Stats.ALL_JUMP:
                            textStat = languages[lang]["ALL_JUMP"];
                            break;
                        case Stats.ALL_FULLTIME:
                            textStat = languages[lang]["ALL_FULLTIME"];
                            break;
                        case Stats.ALL_BESTTIME:
                            textStat = languages[lang]["ALL_BESTTIME"];
                            break;
                        default:
                            break;
                    }
                    texts.Add(textStat);
                }
                tr.changeText(texts);
            }

            foreach (LevelTranslator tr in levelsTranslator)
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

        private void GetStatsTranslators()
        {
            statsTranslator = new List<StatsLevelTranslator>();

            foreach (GameObject gameObject in SceneManager.GetActiveScene().GetRootGameObjects())
                statsTranslator.AddRange(gameObject.GetComponentsInChildren<StatsLevelTranslator>());
        }

        private void GetLevelsTranslators()
        {
            levelsTranslator = new List<LevelTranslator>();

            foreach (GameObject gameObject in SceneManager.GetActiveScene().GetRootGameObjects())
                levelsTranslator.AddRange(gameObject.GetComponentsInChildren<LevelTranslator>());
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