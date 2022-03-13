using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI textComponent => GetComponent<TextMeshProUGUI>();
    private float time;
    private float currentLevelTime;
    private float bestTime;
    private float fullTime;
    private int levelID = -1;
    public bool playing = true;
    private bool isInRun;
    private float runTime = 0f;

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
        else if(levelID != 1 && !isInRun)
        {
            PlayerPrefs.SetInt(Application.version + "IS_PLAYING_RUN", 0);
            PlayerPrefs.SetFloat(Application.version + "ALL_CURRENTRUN", 0f);
        }

        

        if (isInRun)
            runTime = PlayerPrefs.GetFloat(Application.version + "ALL_CURRENTRUN");
        bestTime = PlayerPrefs.GetFloat(Application.version + "LEVEL_BESTTIME" + levelID);
        fullTime = PlayerPrefs.GetFloat(Application.version + "LEVEL_FULLTIME" + levelID);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(Application.version + "IS_PLAYING_RUN", 0);
        PlayerPrefs.SetFloat(Application.version + "ALL_CURRENTRUN", 0f);
        PlayerPrefs.Save();
    }

    public static string ConvertSecToReadable(float sec)
    {
        string minutes = ((int)sec / 60).ToString();
        string seconds = (sec % 60).ToString("f2");
        if (minutes.ToString().Equals("0"))
            return seconds + "s";
        return minutes + "m " + seconds + "s";
    }

    void Update()
    {
        if (playing == true)
        {
            time += Time.deltaTime;
            currentLevelTime += Time.deltaTime;
            updateUI(PlayerPrefs.GetFloat(Application.version + "ALL_CURRENTRUN") + currentLevelTime);
        }
    }

    public void updateUI(float currentRunTime)
    {
        if (isInRun)
            textComponent.SetText(ConvertSecToReadable(time) + "\n" + ConvertSecToReadable(currentRunTime));
        else
            textComponent.SetText(ConvertSecToReadable(time));
    }

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

            if(levelID == 20)
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
