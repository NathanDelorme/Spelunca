using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    /// <summary>
    /// Cette classe permet de donner un comportement à un texte pour qu'il soit considéré comme un timer.
    /// </summary>
    public class Timer : MonoBehaviour
    {
        /// <value>
        /// Texte qui contient le timer.
        /// </value>
        private TextMeshProUGUI textComponent => GetComponent<TextMeshProUGUI>();
        /// <value>
        /// temps de la vie en cours.
        /// </value>
        private float time;
        /// <value>
        /// Temps depuis l'ouverture du niveau.
        /// </value>
        private float currentLevelTime;
        /// <value>
        /// Meilleur temps du niveau courant.
        /// </value>
        private float bestTime;
        /// <value>
        /// Temps total du niveau courant.
        /// </value>
        private float fullTime;
        /// <value>
        /// Identifiant du niveau.
        /// </value>
        private int levelID = -1;
        /// <value>
        /// Variable qui stocke si le jeu est en pause ou non.
        /// </value>
        public bool playing = true;
        /// <value>
        /// Variable qui détermine si le joueur fait une run globale ou non.
        /// </value>
        private bool isInRun;
        /// <value>
        /// Initialisation du temps de la run.
        /// </value>
        private float runTime = 0f;

        /// <summary>
        /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
        /// Cette fonction agit comme un constructeur permettant d'initialiser les attributs et effectuer des actions au chargement du script.
        /// </summary>
        void Start()
        {
            levelID = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5));
            isInRun = PlayerPrefs.GetInt(Application.version + "IS_PLAYING_RUN") == 1;

            time = 0f;
            currentLevelTime = 0f;

            if (levelID == 1)
            {
                isInRun = true;
                if (!PlayerPrefs.HasKey(Application.version + "ALL_BESTRUN"))
                    PlayerPrefs.SetFloat(Application.version + "ALL_BESTRUN", 0f);

                PlayerPrefs.SetInt(Application.version + "IS_PLAYING_RUN", 1);
                PlayerPrefs.SetFloat(Application.version + "ALL_CURRENTRUN", 0f);
            }
            else if (levelID != 1 && !isInRun)
            {
                PlayerPrefs.SetInt(Application.version + "IS_PLAYING_RUN", 0);
                PlayerPrefs.SetFloat(Application.version + "ALL_CURRENTRUN", 0f);
            }



            if (isInRun)
                runTime = PlayerPrefs.GetFloat(Application.version + "ALL_CURRENTRUN");
            bestTime = PlayerPrefs.GetFloat(Application.version + "LEVEL_BESTTIME" + levelID);
            fullTime = PlayerPrefs.GetFloat(Application.version + "LEVEL_FULLTIME" + levelID);
        }

        /// <summary>
        /// Fonction exécuté lorsque l'application est fermé. Cela permet d'arrêter la run en cours.
        /// </summary>
        private void OnApplicationQuit()
        {
            PlayerPrefs.SetInt(Application.version + "IS_PLAYING_RUN", 0);
            PlayerPrefs.SetFloat(Application.version + "ALL_CURRENTRUN", 0f);
            PlayerPrefs.Save();
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

        /// <summary>
        /// Fonction exécuté à chaque frame.
        /// </summary>
        void Update()
        {
            if (playing == true)
            {
                time += Time.deltaTime;
                currentLevelTime += Time.deltaTime;
                updateUI(PlayerPrefs.GetFloat(Application.version + "ALL_CURRENTRUN") + currentLevelTime);
            }
        }

        /// <summary>
        /// Met à jour le texte de l'UI en fonction du temps.
        /// </summary>
        /// <param name="currentRunTime">Temps à afficher.</param>
        public void updateUI(float currentRunTime)
        {
            if (isInRun)
                textComponent.SetText(ConvertSecToReadable(time) + "\n" + ConvertSecToReadable(currentRunTime));
            else
                textComponent.SetText(ConvertSecToReadable(time));
        }

        /// <summary>
        /// Permet de sauvegarder les temps du niveau. (cumul du temps + meilleur temps).
        /// </summary>
        /// <param name="isKilled">Booléen Vrai si le joueur est mort, sinon Faux.</param>
        public void SaveTime(bool isKilled = false)
        {
            playing = false;
            if (isKilled)
            {
                PlayerPrefs.SetFloat(Application.version + "LEVEL_FULLTIME" + levelID, fullTime + time);
                fullTime = PlayerPrefs.GetFloat(Application.version + "LEVEL_FULLTIME" + levelID);
                time = 0f;
                playing = true;
            }
            else
            {
                PlayerPrefs.SetFloat(Application.version + "LEVEL_FULLTIME" + levelID, fullTime + time);

                if (time < bestTime || bestTime == 0f)
                    PlayerPrefs.SetFloat(Application.version + "LEVEL_BESTTIME" + levelID, time);

                Debug.Log(runTime);
                Debug.Log(currentLevelTime);

                if (isInRun)
                    PlayerPrefs.SetFloat(Application.version + "ALL_CURRENTRUN", runTime + currentLevelTime);
                updateUI(PlayerPrefs.GetFloat(Application.version + "ALL_CURRENTRUN"));

                if (levelID == 20)
                {
                    float bestRun = PlayerPrefs.GetFloat(Application.version + "ALL_BESTRUN");
                    float currentRunEnd = PlayerPrefs.GetFloat(Application.version + "ALL_CURRENTRUN");

                    if (bestRun == 0 || bestRun > currentRunEnd)
                    {
                        PlayerPrefs.SetFloat(Application.version + "ALL_BESTRUN", currentRunEnd);
                        PlayerPrefs.SetFloat(Application.version + "ALL_CURRENTRUN", 0f);
                        PlayerPrefs.SetInt(Application.version + "IS_PLAYING_RUN", 0);
                    }
                }
            }

            PlayerPrefs.Save();
        }
    }
}