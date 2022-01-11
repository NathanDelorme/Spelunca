using UnityEngine;

namespace UI
{
    public class UIScript : MonoBehaviour
    {
        public UIType UIType;
        private GameObject obj;

        private void Awake()
        {
            obj = this.gameObject;
        }

        public void Open()
        {
            if (!obj.activeSelf)
                obj.SetActive(true);
        }

        public void Close()
        {
            if (obj.activeSelf)
                obj.SetActive(false);
        }

        public bool IsOpen()
        {
            return obj.activeSelf;
        }
    }
}
