using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Classe permettant d'afficher les différentes images liées à l'histoire.
/// </summary>
public class SlideImageUI : MonoBehaviour
{
    /// <value>
    /// Images à afficher.
    /// </value>
    public List<Sprite> images;
    /// <value>
    /// Texte à afficher sur une des images.
    /// </value>
    public TextMeshProUGUI text;
    /// <value>
    /// Image par défaut.
    /// </value>
    public Image imageUi;
    /// <value>
    /// Scène à ouvrir après avoir visionné les images.
    /// </value>
    public string switchSceneOnFinished;
    /// <value>
    /// Image sur laquelle le texte doit apparaître.
    /// </value>
    public int indexTextApparition = 1;
    /// <value>
    /// Index courant de l'image.
    /// </value>
    private int index = 0;

    /// <summary>
    /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
    /// </summary>
    public void Start()
    {
        text.enabled = index == indexTextApparition;
    }

    /// <summary>
    /// Permet de passer à l'image suivante ou de charger le niveau.
    /// </summary>
    public void Next()
    {
        index++;
        text.enabled = index == indexTextApparition;

        if (index >= images.Count)
            SceneManager.LoadScene(switchSceneOnFinished);
        else
            imageUi.sprite = images[index];
    }
}
