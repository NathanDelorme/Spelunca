using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SaveSystem
{
    /// <summary>
    /// Classe permettant la sauvegarde de la configuration des touches choisies par le joueur.
    /// Ce script provient d'internet.
    /// </summary>
    public class SaveInputSystem : MonoBehaviour
    {
        /// <value>
        /// Fichier des configurations des touches du joueur.
        /// </value>
        public InputActionAsset control;

        /// <summary>
        /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
        /// </summary>
        private void Start()
        {
            LoadControlOverrides();
        }

        /// <summary>
        /// Fonction qui initialise la serialization du changements des touches.
        /// </summary>
        [System.Serializable]
        class BindingWrapperClass
        {
            public List<BindingSerializable> bindingList = new List<BindingSerializable>();
        }

        /// <summary>
        /// Structure permettant de stocker un identifiant pour une liste via un chemin.
        /// </summary>
        [System.Serializable]
        private struct BindingSerializable
        {
            public string id;
            public string path;

            public BindingSerializable(string bindingId, string bindingPath)
            {
                id = bindingId;
                path = bindingPath;
            }
        }

        /// <summary>
        /// Enregistre les remplacements de touches fait par le joueur dans les PlayerPrefs.
        /// </summary>
        public void StoreControlOverrides()
        {
            BindingWrapperClass bindingList = new BindingWrapperClass();
            foreach (var map in control.actionMaps)
            {
                foreach (var binding in map.bindings)
                {
                    if (!string.IsNullOrEmpty(binding.overridePath))
                    {
                        bindingList.bindingList.Add(new BindingSerializable(binding.id.ToString(), binding.overridePath));
                    }
                }
            }

            PlayerPrefs.SetString(Application.version + "ControlOverrides", JsonUtility.ToJson(bindingList));
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Charge les contrôles sauvegardé dans les PlayerPrefs.
        /// </summary>
        public void LoadControlOverrides()
        {
            if (PlayerPrefs.HasKey(Application.version + "ControlOverrides"))
            {
                BindingWrapperClass bindingList = JsonUtility.FromJson(PlayerPrefs.GetString(Application.version + "ControlOverrides"), typeof(BindingWrapperClass)) as BindingWrapperClass;
                Dictionary<System.Guid, string> overrides = new Dictionary<System.Guid, string>();

                foreach (var item in bindingList.bindingList)
                    overrides.Add(new System.Guid(item.id), item.path);

                foreach (var map in control.actionMaps)
                {
                    var bindings = map.bindings;
                    for (var i = 0; i < bindings.Count; ++i)
                        if (overrides.TryGetValue(bindings[i].id, out string overridePath))
                            map.ApplyBindingOverride(i, new InputBinding { overridePath = overridePath });
                }
            }
        }
    }
}