using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using static Language.StatsLevelTranslator;

namespace Language
{
    /// <summary>
    /// Classe qui s'occupe de gérer les différentes langues présentent en jeu.
    /// </summary>
    public class LanguageManager : MonoBehaviour
    {
        /// <value>
        /// Fichier XML où sont stockées les traductions.
        /// </value>
        public TextAsset xmlFile;
        /// <value>
        /// Structure de données permettant d'avoir, en fonction de la langue et de l'identifiant du texte, la bonne traduction.
        /// </value>
        private Dictionary<string, Dictionary<string, string>> languages = new Dictionary<string, Dictionary<string, string>>();
        /// <value>
        /// Liste de tous les textes basiques à traduire dans le niveau courant.
        /// </value>
        private List<Translator> translators;
        /// <value>
        /// Liste de tous les textes liés aux statistiques à traduire dans le niveau courant.
        /// </value>
        private List<StatsLevelTranslator> statsTranslator;
        /// <value>
        /// Liste de tous les textes liés à l'affichage des niveaux à traduire dans le niveau courant.
        /// </value>
        private List<LevelTranslator> levelsTranslator;
        /// <value>
        /// Langage par défaut.
        /// </value>
        private string lastLanguage = "EN";

        /// <summary>
        /// Fonction appelé à chaque chargement du script.
        /// </summary>
        private void Awake()
        {
            XMLReader();
        }

        /// <summary>
        /// Fonction appelé quand l'objet passe de "désactivé" à "activé".
        /// </summary>
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelIsLoaded;
        }

        /// <summary>
        /// Fonction appelé quand l'objet passe de "activé" à "désactivé".
        /// </summary>
        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelIsLoaded;
        }

        /// <summary>
        /// Fonction appelé dès qu'un niveau est chargé afin d'initialiser le LanguageManager.
        /// </summary>
        /// <param name="scene">Objet qui représente la Scene.</param>
        /// <param name="mode"></param>
        private void OnLevelIsLoaded(Scene scene, LoadSceneMode mode)
        {
            GetTranslators();
            GetStatsTranslators();
            GetLevelsTranslators();
            ApplyLanguage("EN");
        }

        /// <summary>
        /// Permet d'appliquer le changement de language lors de la selection d'une nouvelle langue dans les paramètres.
        /// </summary>
        /// <param name="lang">Identifiant de la langue (exemple : FR, EN, ES, BZ).</param>
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
                        case Stats.ALL_BESTRUN:
                            textStat = languages[lang]["ALL_BESTRUN"];
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

        /// <summary>
        /// Récupère la liste des textes basiques à traduire.
        /// </summary>
        private void GetTranslators()
        {
            translators = new List<Translator>();

            foreach (GameObject gameObject in SceneManager.GetActiveScene().GetRootGameObjects())
                translators.AddRange(gameObject.GetComponentsInChildren<Translator>());
        }

        /// <summary>
        /// Récupère la liste des textes liés aux statistiques à traduire.
        /// </summary>
        private void GetStatsTranslators()
        {
            statsTranslator = new List<StatsLevelTranslator>();

            foreach (GameObject gameObject in SceneManager.GetActiveScene().GetRootGameObjects())
                statsTranslator.AddRange(gameObject.GetComponentsInChildren<StatsLevelTranslator>());
        }

        /// <summary>
        /// Récupère la liste des textes liés à l'affichage du niveau à traduire.
        /// </summary>
        private void GetLevelsTranslators()
        {
            levelsTranslator = new List<LevelTranslator>();

            foreach (GameObject gameObject in SceneManager.GetActiveScene().GetRootGameObjects())
                levelsTranslator.AddRange(gameObject.GetComponentsInChildren<LevelTranslator>());
        }

        /// <summary>
        /// Permet la lecture, le traitement et donc le parsing des données présentent dans le fichier XML des langues.
        /// Les données sont stocké dans la structure de données <c>languages</c>.
        /// </summary>
        private void XMLReader()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNodeList langList = doc.GetElementsByTagName("lang");

            foreach (XmlNode lang in langList)
            {
                XmlNodeList langContent = lang.ChildNodes;
                Dictionary<string, string> dict = new Dictionary<string, string>();

                foreach (XmlNode textNode in langContent)
                    dict.Add(textNode.Attributes[0].Value, textNode.InnerText);

                languages.Add(lang.Attributes[0].Value, dict);
            }
        }
    }
}