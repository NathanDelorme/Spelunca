using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Classe permettant la gestion des différentes interface utilisateur.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        /// <value>
        /// Menu ouvert par défaut.
        /// </value>
        public UIType defaultUIType = UIType.None;
        /// <value>
        /// Dictionnaire de tous les menus.
        /// </value>
        private Dictionary<UIType, UIScript> UIDict;

        /// <summary>
        /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
        /// </summary>
        private void Start()
        {
            UIDict = new Dictionary<UIType, UIScript>();
            UIScript[] UIsList = FindObjectsOfType<UIScript>();
            UIScript UIToOpen = null;

            foreach(UIScript UI in UIsList)
            {
                UIDict.Add(UI.UIType, UI);

                if (UI.UIType == defaultUIType)
                    UIToOpen = UI;
            }

            if (UIToOpen)
                OpenScreen(UIToOpen);
        }

        /// <summary>
        /// Ferme tout les menus et ouvre celui désiré.
        /// </summary>
        /// <param name="targetScript">Script du menu à ouvrir.</param>
        public void OpenScreen(UIScript targetScript)
        {
            UIType targetUI = targetScript.UIType;

            foreach (var UI in UIDict)
            {
                if(UI.Value.UIType != targetUI)
                    UI.Value.Close();
                else
                    UI.Value.Open();
            }
        }
    }
}
