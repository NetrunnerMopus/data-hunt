using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class ServerRow : MonoBehaviour
    {
        private HorizontalLayoutGroup layout;

        void Awake()
        {
            layout = gameObject.AddComponent<HorizontalLayoutGroup>();
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;
            layout.childAlignment = TextAnchor.MiddleRight;
            layout.spacing = 4;
        }

        internal Server CreateServer(string serverName)
        {
            var gameObject = new GameObject(serverName);
            Server server = gameObject.AddComponent<Server>();
            gameObject.transform.SetParent(transform, false);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout.GetComponent<RectTransform>());
            return server;
        }
    }
}