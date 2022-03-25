using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Classe permettant la gestion des paramètre liés à l'écran. (Résolution, plein écran).
/// </summary>
public class ScreenUI : MonoBehaviour
{
    /// <value>
    /// Objet qui stocke les paramètre du joueur en temps réel.
    /// </value>
    public SettingsData settingsData;
    /// <value>
    /// Toggle permettant d'activer/désactiver le mode plein écran.
    /// </value>
    public Toggle fullscreenToggle;
    /// <value>
    /// Dropdown menu permettant le changement de la résolution.
    /// </value>
    public TMP_Dropdown screenResolutionDropdown;
    /// <value>
    /// Liste des largeurs des résolutions disponibles à la sélection.
    /// </value>
    public List<int> widths = new List<int>() {1280, 1366, 1600, 1920, 2560, 3840};
    /// <value>
    /// Liste des hauteurs des résolutions disponibles à la sélection.
    /// </value>
    public List<int> heights = new List<int>() {720, 768, 900, 1080, 1440, 2160};
    /// <summary>
    /// Index de l'item sélectionné.
    /// </summary>
    public int dropdownSelectedValue = 0;

    /// <summary>
    /// Fonction appelée lorsque l'object passe de "désactivé" à "activé".
    /// </summary>
    public void OnEnable()
    {
        UpdateUI();
    }

    /// <summary>
    /// Applique les paramètre pour mette le jeu en plein écran ou non.
    /// </summary>
    public void ChangeFullscreen()
    {
        settingsData.isFullscreen = !settingsData.isFullscreen;
    }

    /// <summary>
    /// Mets à jour l'interface en fonction des dernière valeurs sauvegardées.
    /// </summary>
    public void UpdateUI()
    {
        fullscreenToggle.SetIsOnWithoutNotify(settingsData.isFullscreen);
        getSelectedValueFromSettings();
        screenResolutionDropdown.SetValueWithoutNotify(dropdownSelectedValue);
    }

    /// <summary>
    /// Change la résolution de l'écran en fonction de ce qui est sélectionné dans le Dropdown menu <c>screenResolutionDropdown</c>.
    /// </summary>
    public void ChangeScreenSize()
    {
        dropdownSelectedValue = screenResolutionDropdown.value;
        settingsData.resolutionWidth = widths[dropdownSelectedValue];
        settingsData.resolutionheight = heights[dropdownSelectedValue];
    }

    /// <summary>
    /// Récupère la résolution souhaité en fonction de l'index de l'item sélectionné dans le Dropdown menu <c>screenResolutionDropdown</c>.
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
