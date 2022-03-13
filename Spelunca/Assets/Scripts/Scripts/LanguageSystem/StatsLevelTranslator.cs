using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsLevelTranslator : MonoBehaviour
{
    public enum Stats
    {
        LEVEL_DEATHS,
        LEVEL_JUMP,
        LEVEL_FULLTIME,
        LEVEL_BESTTIME,

        ALL_DEATHS,
        ALL_JUMP,
        ALL_FULLTIME,
        ALL_BESTTIME
    }

    public int levelID = 0;
    public List<Stats> statsToDisplay;
    private List<string> texts;
    private List<string> defaultTexts;
    private TextMeshProUGUI textComponent => GetComponent<TextMeshProUGUI>();

    void Start()
    {
        if (!textComponent)
            Debug.LogError("Translator - textComponent is null.");
        else if(levelID == -1 || levelID == -2)
        {
            if (levelID == -2)
                levelID = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5));
            loadMenu();
        }
    }

    private void InitializeLevelStats()
    {
        string saveId;

        if(levelID != -1)
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
        }
    }

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
                default:
                    break;
            }
            if (i < texts.Count-1)
                texts[i] += (data + "\n\n\n");
            else
                texts[i] += data;
            i += 1;
        }

        textComponent.SetText("");
        foreach (string s in texts)
            textComponent.SetText(textComponent.text + s);
    }

    public int GetTotalIntCount(string saveType)
    {
        int res = 0;
        for(int i = 1; i <= 20; i++)
            res += PlayerPrefs.GetInt(Application.version + saveType + i);
        return res;
    }

    public float GetTotalFloatCount(string saveType)
    {
        float res = 0;
        for (int i = 1; i <= 20; i++)
            res += PlayerPrefs.GetFloat(Application.version + saveType + i);
        return res;
    }

    public void changeText(List<string> translatedTexts)
    {
        defaultTexts = new List<string>();
        defaultTexts = translatedTexts;
        loadMenu();
    }

    public static string ConvertSecToReadable(float sec)
    {
        string minutes = ((int)sec / 60).ToString();
        string seconds = (sec % 60).ToString("f2");
        if (minutes.ToString().Equals("0"))
            return seconds + "s";
        return minutes + "m " + seconds + "s";
    }
}
