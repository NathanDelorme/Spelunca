using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Classe permettant la gestion des param�tre li�s � l'�cran. (R�solution, plein �cran).
/// </summary>
public class ScreenUI : MonoBehaviour
{
    /// <value>
    /// Objet qui stocke les param�tre du joueur en temps r�el.
    /// </value>
    public SettingsData settingsData;
    /// <value>
    /// Toggle permettant d'activer/d�sactiver le mode plein �cran.
    /// </value>
    public Toggle fullscreenToggle;
    /// <value>
    /// Dropdown menu permettant le changement de la r�solution.
    /// </value>
    public TMP_Dropdown screenResolutionDropdown;
    /// <value>
    /// Liste des largeurs des r�solutions disponibles � la s�lection.
    /// </value>
    public List<int> widths = new List<int>() {1280, 1366, 1600, 1920, 2560, 3840};
    /// <value>
    /// Liste des hauteurs des r�solutions disponibles � la s�lection.
    /// </value>
    public List<int> heights = new List<int>() {720, 768, 900, 1080, 1440, 2160};
    /// <summary>
    /// Index de l'item s�lectionn�.
    /// </summary>
    public int dropdownSelectedValue = 0;

    /// <summary>
    /// Fonction appel�e lorsque l'object passe de "d�sactiv�" � "activ�".
    /// </summary>
    public void OnEnable()
    {
        UpdateUI();
    }

    /// <summary>
    /// Applique les param�tre pour mette le jeu en plein �cran ou non.
    /// </summary>
    public void ChangeFullscreen()
    {
        settingsData.isFullscreen = !settingsData.isFullscreen;
    }

    /// <summary>
    /// Mets � jour l'interface en fonction des derni�re valeurs sauvegard�es.
    /// </summary>
    public void UpdateUI()
    {
        fullscreenToggle.SetIsOnWithoutNotify(settingsData.isFullscreen);
        getSelectedValueFromSettings();
        screenResolutionDropdown.SetValueWithoutNotify(dropdownSelectedValue);
    }

    /// <summary>
    /// Change la r�solution de l'�cran en fonction de ce qui est s�lectionn� dans le Dropdown menu <c>screenResolutionDropdown</c>.
    /// </summary>
    public void ChangeScreenSize()
    {
        dropdownSelectedValue = screenResolutionDropdown.value;
        settingsData.resolutionWidth = widths[dropdownSelectedValue];
        settingsData.resolutionheight = heights[dropdownSelectedValue];
    }

    /// <summary>
    /// R�cup�re la r�solution souhait� en fonction de l'index de l'item s�lectionn� dans le Dropdown menu <c>screenResolutionDropdown</c>.
    /// </summary>
    public void getSelectedValueFromSettings()
    {
        switch(settingsData.resolutionWidth)
        {
            case 1280:
                dropdownSelectedValue = 0;
                break;
            case 1366:
                dropdownSelectedValue = 1;
                break;
            case 1600:
                dropdownSelectedValue = 2;
                break;
            case 1920:
                dropdownSelectedValue = 3;
                break;
            case 2560:
                dropdownSelectedValue = 4;
                break;
            case 3840:
                dropdownSelectedValue = 5;
                break;
            default:
                dropdownSelectedValue = 3;
                break;
        }
    }
}
