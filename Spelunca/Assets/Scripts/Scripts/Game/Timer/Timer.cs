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
    private float bestTime;
    private float fullTime;
    private int levelID = -1;
    public bool playing = true;

    void Start()
    {
        levelID = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5));

        time = 0;
        bestTime = PlayerPrefs.GetFloat("LEVEL_BESTTIME" + levelID);
        fullTime = PlayerPrefs.GetFloat("LEVEL_FULLTIME" + levelID);
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
            textComponent.SetText(ConvertSecToReadable(time));
        }
    }

    public void SaveTime(bool isKilled = false)
    {
        playing = false;
        if (isKilled)
        {
            PlayerPrefs.SetFloat("LEVEL_FULLTIME" + levelID, fullTime + time);
            fullTime = PlayerPrefs.GetFloat("LEVEL_FULLTIME" + levelID);
            time = 0f;
        }
        else
        {
            PlayerPrefs.SetFloat("LEVEL_FULLTIME" + levelID, fullTime + time);

            if (time < bestTime || bestTime == 0f)
                PlayerPrefs.SetFloat("LEVEL_BESTTIME" + levelID, time); ;
        }
        playing = true;
    }
}
