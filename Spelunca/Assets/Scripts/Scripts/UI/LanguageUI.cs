using System;
using System.Collections;
using System.Collections.Generic;
using Language;
using UnityEngine;
using UnityEngine.UI;

public class LanguageUI : MonoBehaviour
{
    public SettingsSO settingsSO;
    public List<Toggle> toggles;
    public List<Sprite> sprites;
    public List<Sprite> spritesSelected;
    public List<string> languages;

    private LanguageManager languageManager;

    public void Start()
    {
        languageManager = FindObjectOfType<LanguageManager>();
        languageManager.ApplyLanguage(settingsSO.data.language);
    }

    public void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            if (languages[i] == settingsSO.data.language)
            {
                toggles[i].SetIsOnWithoutNotify(true);
                toggles[i].gameObject.GetComponent<Image>().sprite = spritesSelected[i];
            }
            else
            {
                toggles[i].SetIsOnWithoutNotify(false);
                toggles[i].gameObject.GetComponent<Image>().sprite = sprites[i];
            }
        }
    }
}
