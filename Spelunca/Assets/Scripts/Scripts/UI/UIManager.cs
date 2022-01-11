using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public UIType defaultUIType = UIType.None;
        private Dictionary<UIType, UIScript> UIDict;

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
