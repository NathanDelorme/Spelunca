using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Classe permettant d'afficher les diff�rentes images li�es � l'histoire.
/// </summary>
public class SlideImageUI : MonoBehaviour
{
    /// <value>
    /// Images � afficher.
    /// </value>
    public List<Sprite> images;
    /// <value>
    /// Texte � afficher sur une des images.
    /// </value>
    public TextMeshProUGUI text;
    /// <value>
    /// Image par d�faut.
    /// </value>
    public Image imageUi;
    /// <value>
    /// Sc�ne � ouvrir apr�s avoir visionn� les images.
    /// </value>
    public string switchSceneOnFinished;
    /// <value>
    /// Image sur laquelle le texte doit appara�tre.
    /// </value>
    public int indexTextApparition = 1;
    /// <value>
    /// Index courant de l'image.
    /// </value>
    private int index = 0;

    /// <summary>
    /// Fonction ex�cut� avant la premi�re frame du programme, donc avant le premier appel � Update.
    /// </summary>
    public void Start()
    {
        text.enabled = index == indexTextApparition;
    }

    /// <summary>
    /// Permet de passer � l'image suivante ou de charger le niveau.
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
