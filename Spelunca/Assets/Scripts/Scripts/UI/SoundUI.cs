using Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Classe permettant la gestion des paramètre du son.
    /// </summary>
    public class SoundUI : MonoBehaviour
    {
        /// <value>
        /// Objet qui stocke les paramètre du joueur en temps réel.
        /// </value>
        public SettingsData settingsData;
        /// <value>
        /// Liste des sliders (sliders pour gérer la musique ou les effets sonores).
        /// </value>
        public List<Slider> sliders;

        /// <summary>
        /// Fonction appelée lorsque l'object passe de "désactivé" à "activé".
        /// </summary>
        public void OnEnable()
        {
            UpdateUI();
        }

        /// <summary>
        /// Met à jour l'interface en fonction des dernière valeurs sauvegardées.
        /// </summary>
        public void UpdateUI()
        {
            sliders[0].SetValueWithoutNotify(settingsData.musicVolume);
            sliders[1].SetValueWithoutNotify(settingsData.sfxVolume);
        }

        /// <summary>
        /// Change le volume en fonction des valeurs sélectionnées psur les sliders.
        /// </summary>
        public void ChangeVolume()
        {
            settingsData.musicVolume = sliders[0].value;
            settingsData.sfxVolume = sliders[1].value;
        }
    }
}

