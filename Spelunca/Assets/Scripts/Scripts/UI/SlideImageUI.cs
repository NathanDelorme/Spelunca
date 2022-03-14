using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SlideImageUI : MonoBehaviour
{
    public List<Sprite> images;
    public TextMeshProUGUI text;
    public Image imageUi;
    public string switchSceneOnFinished;
    public int indexTextApparition = 1;

    private int index = 0;

    public void Start()
    {
        text.enabled = index == indexTextApparition;
    }

    public void Next()
    {
        index++;
        text.enabled = index == indexTextApparition;

        if (index >= images.Count)
        {
            SceneManager.LoadScene(switchSceneOnFinished);
        }
        else
        {
            imageUi.sprite = images[index];
        }
    }
}
