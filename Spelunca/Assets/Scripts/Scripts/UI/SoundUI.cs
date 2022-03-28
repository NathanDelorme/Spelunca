using Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Classe permettant la gestion des param�tre du son.
    /// </summary>
    public class SoundUI : MonoBehaviour
    {
        /// <value>
        /// Objet qui stocke les param�tre du joueur en temps r�el.
        /// </value>
        public SettingsData settingsData;
        /// <value>
        /// Liste des sliders (sliders pour g�rer la musique ou les effets sonores).
        /// </value>
        public List<Slider> sliders;

        /// <summary>
        /// Fonction appel�e lorsque l'object passe de "d�sactiv�" � "activ�".
        /// </summary>
        public void OnEnable()
        {
            UpdateUI();
        }

        /// <summary>
        /// Mets � jour l'interface en fonction des derni�re valeurs sauvegard�es.
        /// </summary>
        public void UpdateUI()
        {
            sliders[0].SetValueWithoutNotify(settingsData.musicVolume);
            sliders[1].SetValueWithoutNotify(settingsData.sfxVolume);
        }

        /// <summary>
        /// Change le volume en fonction des valeurs s�lectionn�es psur les sliders.
        /// </summary>
        public void ChangeVolume()
        {
            settingsData.musicVolume = sliders[0].value;
            settingsData.sfxVolume = sliders[1].value;
        }
    }
}

