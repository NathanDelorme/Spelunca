using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Language
{
    /// <summary>
    /// Classe permettant la traduction d'un texte contenant des statistiques liées joueurs..
    /// </summary>
    public class StatsLevelTranslator : MonoBehaviour
    {
        /// <summary>
        /// Enumeration des statistiques qui peuvent être affichées.
        /// </summary>
        public enum Stats
        {
            LEVEL_DEATHS,
            LEVEL_JUMP,
            LEVEL_FULLTIME,
            LEVEL_BESTTIME,

            ALL_DEATHS,
            ALL_JUMP,
            ALL_FULLTIME,
            ALL_BESTTIME,
            ALL_BESTRUN
        }

        /// <value>
        /// Identifiant du niveau courant.
        /// </value>
        public int levelID = 0;
        /// <value>
        /// Liste des types de statistiques à afficher dans le texte.
        /// </value>
        public List<Stats> statsToDisplay;
        /// <value>
        /// Textes à afficher.
        /// </value>
        private List<string> texts;
        /// <value>
        /// Textes par défaut.
        /// </value>
        private List<string> defaultTexts;
        /// <value>
        /// Component qui affiche le texte au joueur.
        /// </value>
        private TextMeshProUGUI textComponent => GetComponent<TextMeshProUGUI>();

        /// <summary>
        /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
        /// </summary>
        void Start()
        {
            if (!textComponent)
                Debug.LogError("Translator - textComponent is null.");
            else if (levelID == -1 || levelID == -2)
            {
                if (levelID == -2)
                    levelID = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5));
                loadMenu();
            }
        }

        /// <summary>
        /// Fonction qui initialise les statistiques d'un niveau si elle n'ont pas déjà été initialisées.
        /// </summary>
        private void InitializeLevelStats()
        {
            string saveId;

            if (levelID != -1)
            {
                saveId = Application.version + "LEVEL_DEATHS" + levelID;
                if (!PlayerPrefs.HasKey(saveId))
                    PlayerPrefs.SetInt(saveId, 0);

                saveId = Application.version + "LEVEL_JUMP" + levelID;
                if (!PlayerPrefs.HasKey(saveId))
                    PlayerPrefs.SetInt(saveId, 0);

                saveId = Application.version + "LEVEL_FULLTIME" + levelID;
                if (!PlayerPrefs.HasKey(saveId))
                    PlayerPrefs.SetFloat(saveId, 0f);

                saveId = Application.version + "LEVEL_BESTTIME" + levelID;
                if (!PlayerPrefs.HasKey(saveId))
                    PlayerPrefs.SetFloat(saveId, 0f);
            }
            else
            {
                saveId = Application.version + "ALL_DEATHS";
                if (!PlayerPrefs.HasKey(saveId))
                    PlayerPrefs.SetInt(saveId, 0);

                saveId = Application.version + "ALL_JUMP";
                if (!PlayerPrefs.HasKey(saveId))
                    PlayerPrefs.SetInt(saveId, 0);

                saveId = Application.version + "ALL_FULLTIME";
                if (!PlayerPrefs.HasKey(saveId))
                    PlayerPrefs.SetFloat(saveId, 0f);

                saveId = Application.version + "ALL_BESTTIME";
                if (!PlayerPrefs.HasKey(saveId))
                    PlayerPrefs.SetFloat(saveId, 0f);

                saveId = Application.version + "ALL_BESTRUN";
                if (!PlayerPrefs.HasKey(saveId))
                    PlayerPrefs.SetFloat(saveId, 0f);
            }
        }

        /// <summary>
        /// Affiche le texte souhaité lorsque le menu est chargé aux yeux du joueur.
        /// </summary>
        public void loadMenu()
        {
            InitializeLevelStats();

            texts = new List<string>();
            texts.AddRange(defaultTexts);
            string saveId;
            int i = 0;

            foreach (Stats stat in statsToDisplay)
            {
                string data = "";
                float time = 0f;
                switch (stat)
                {
                    case Stats.LEVEL_DEATHS:
                        saveId = Application.version + "LEVEL_DEATHS" + levelID;
                        data = PlayerPrefs.GetInt(saveId).ToString();
                        break;

                    case Stats.LEVEL_JUMP:
                        saveId = Application.version + "LEVEL_JUMP" + levelID;
                        data = PlayerPrefs.GetInt(saveId).ToString();
                        break;

                    case Stats.LEVEL_FULLTIME:
                        saveId = Application.version + "LEVEL_FULLTIME" + levelID;

                        if (PlayerPrefs.GetFloat(saveId) <= 0f)
                            data = "--";
                        else
                            data = ConvertSecToReadable(PlayerPrefs.GetFloat(saveId));
                        break;

                    case Stats.LEVEL_BESTTIME:
                        saveId = Application.version + "LEVEL_BESTTIME" + levelID;

                        if (PlayerPrefs.GetFloat(saveId) <= 0f)
                            data = "--";
                        else
                            data = ConvertSecToReadable(PlayerPrefs.GetFloat(saveId));
                        break;


                    case Stats.ALL_DEATHS:
                        saveId = Application.version + "ALL_DEATHS";
                        data = GetTotalIntCount("LEVEL_DEATHS").ToString();
                        break;

                    case Stats.ALL_JUMP:
                        saveId = Application.version + "ALL_JUMP";
                        data = GetTotalIntCount("LEVEL_JUMP").ToString();
                        break;

                    case Stats.ALL_FULLTIME:
                        saveId = Application.version + "ALL_FULLTIME";
                        time = GetTotalFloatCount("LEVEL_FULLTIME");
                        if (time <= 0f)
                            data = "--";
                        else
                            data = ConvertSecToReadable(time);
                        break;

                    case Stats.ALL_BESTTIME:
                        saveId = Application.version + "LEVEL_BESTTIME20";
                        if (PlayerPrefs.GetFloat(saveId) <= 0f)
                            textComponent.color = Color.red;
                        else
                            textComponent.color = Color.white;

                        saveId = Application.version + "ALL_BESTTIME";
                        time = GetTotalFloatCount("LEVEL_BESTTIME");
                        if (time <= 0f)
                            data = "--";
                        else
                            data = ConvertSecToReadable(time);
                        break;

                    case Stats.ALL_BESTRUN:
                        saveId = Application.version + "ALL_BESTRUN";

                        time = PlayerPrefs.GetFloat(saveId);
                        if (time <= 0f)
                            data = "--";
                        else
                            data = ConvertSecToReadable(time);
                        break;
                    default:
                        break;
                }
                if (i < texts.Count - 1)
                    texts[i] += (data + "\n\n\n");
                else
                    texts[i] += data;
                i += 1;
            }

            textComponent.SetText("");
            foreach (string s in texts)
                textComponent.SetText(textComponent.text + s);
        }

        /// <summary>
        /// Fonction qui calcul la sommes de toutes les valeurs d'une statistique précise.
        /// </summary>
        /// <param name="saveType">Statistique à sommer.</param>
        /// <returns>Somme des valeurs.</returns>
        public int GetTotalIntCount(string saveType)
        {
            int res = 0;
            for (int i = 1; i <= 20; i++)
                res += PlayerPrefs.GetInt(Application.version + saveType + i);
            return res;
        }

        /// <summary>
        /// Fonction qui calcul la sommes de toutes les valeurs d'une statistique précise.
        /// </summary>
        /// <param name="saveType">Statistique à sommer.</param>
        /// <returns>Somme des valeurs.</returns>
        public float GetTotalFloatCount(string saveType)
        {
            float res = 0;
            for (int i = 1; i <= 20; i++)
                res += PlayerPrefs.GetFloat(Application.version + saveType + i);
            return res;
        }

        /// <summary>
        /// Permet de changer le texte du composant par la traduction souhaitée.
        /// </summary>
        /// <param name="translatedText">Texte à afficher.</param>
        public void changeText(List<string> translatedTexts)
        {
            defaultTexts = new List<string>();
            defaultTexts = translatedTexts;
            loadMenu();
        }

        /// <summary>
        /// Fonction qui converti un nombre de seconde en chaine de caratère.
        /// </summary>
        /// <param name="sec">Secondes à convertir.</param>
        /// <returns>Chaine de caractère de la forme : "mm:ss.ms"</returns>
        public static string ConvertSecToReadable(float sec)
        {
            string minutes = ((int)sec / 60).ToString();
            string seconds = (sec % 60).ToString("f2");
            if (minutes.ToString().Equals("0"))
                return seconds + "s";
            return minutes + "m " + seconds + "s";
        }
    }
}
