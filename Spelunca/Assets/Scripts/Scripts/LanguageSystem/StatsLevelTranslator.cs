using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

        if(levelID == -1)
            loadMenu();
    }

    private void InitializeLevelStats()
    {
        string saveId;

        if(levelID != -1)
        {
            saveId = "LEVEL_DEATHS" + levelID;
            if (!PlayerPrefs.HasKey(saveId))
                PlayerPrefs.SetInt(saveId, 0);

            saveId = "LEVEL_JUMP" + levelID;
            if (!PlayerPrefs.HasKey(saveId))
                PlayerPrefs.SetInt(saveId, 0);

            saveId = "LEVEL_FULLTIME" + levelID;
            if (!PlayerPrefs.HasKey(saveId))
                PlayerPrefs.SetFloat(saveId, 0f);

            saveId = "LEVEL_BESTTIME" + levelID;
            if (!PlayerPrefs.HasKey(saveId))
                PlayerPrefs.SetFloat(saveId, 0f);
        }
        else
        {
            saveId = "ALL_DEATHS";
            if (!PlayerPrefs.HasKey(saveId))
                PlayerPrefs.SetInt(saveId, 0);

            saveId = "ALL_JUMP";
            if (!PlayerPrefs.HasKey(saveId))
                PlayerPrefs.SetInt(saveId, 0);

            saveId = "ALL_FULLTIME";
            if (!PlayerPrefs.HasKey(saveId))
                PlayerPrefs.SetFloat(saveId, 0f);

            saveId = "ALL_BESTTIME";
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

            switch (stat)
            {
                case Stats.LEVEL_DEATHS:
                    saveId = "LEVEL_DEATHS" + levelID;
                    data = PlayerPrefs.GetInt(saveId).ToString();
                    break;
                case Stats.LEVEL_JUMP:
                    saveId = "LEVEL_JUMP" + levelID;
                    data = PlayerPrefs.GetInt(saveId).ToString();
                    break;
                case Stats.LEVEL_FULLTIME:
                    saveId = "LEVEL_FULLTIME" + levelID;

                    if (PlayerPrefs.GetFloat(saveId) <= 0f)
                        data = "----";
                    else
                        data = ConvertSecToReadable(PlayerPrefs.GetFloat(saveId));
                    break;
                case Stats.LEVEL_BESTTIME:
                    saveId = "LEVEL_BESTTIME" + levelID;

                    if (PlayerPrefs.GetFloat(saveId) <= 0f)
                        data = "----";
                    else
                        data = ConvertSecToReadable(PlayerPrefs.GetFloat(saveId));
                    break;
                case Stats.ALL_DEATHS:
                    saveId = "ALL_DEATHS";
                    data = PlayerPrefs.GetInt(saveId).ToString();
                    break;
                case Stats.ALL_JUMP:
                    saveId = "ALL_JUMP";
                    data = PlayerPrefs.GetInt(saveId).ToString();
                    break;
                case Stats.ALL_FULLTIME:
                    saveId = "ALL_FULLTIME";

                    if (PlayerPrefs.GetFloat(saveId) <= 0f)
                        data = "----";
                    else
                        data = ConvertSecToReadable(PlayerPrefs.GetFloat(saveId));
                    break;
                case Stats.ALL_BESTTIME:
                    saveId = "ALL_BESTTIME";

                    if (PlayerPrefs.GetFloat(saveId) <= 0f)
                        data = "----";
                    else
                        data = ConvertSecToReadable(PlayerPrefs.GetFloat(saveId));
                    break;
                default:
                    break;
            }
            if (i < texts.Count-1)
                texts[i] += (data + "\n\n");
            else
                texts[i] += data;
            i += 1;
        }

        textComponent.SetText("");
        foreach (string s in texts)
            textComponent.SetText(textComponent.text + s);
    }

    public void changeText(List<string> translatedTexts)
    {
        defaultTexts = new List<string>();
        defaultTexts = translatedTexts;
    }

    public static string ConvertSecToReadable(float sec)
    {
        string minutes = ((int)sec / 60).ToString();
        string seconds = (sec % 60).ToString("f2");
        return minutes + "m " + seconds + "s";
    }
}
