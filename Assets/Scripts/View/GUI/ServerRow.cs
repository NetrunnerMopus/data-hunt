using model.zones.corp;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class ServerRow : MonoBehaviour, IServerCreationObserver
    {
        private HorizontalLayoutGroup layout;
        public readonly IDictionary<IServer, ServerBox> boxes = new Dictionary<IServer, ServerBox>();

        public void Represent(Zones zones)
        {
            zones.ObserveServerCreation(this);
        }

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

        public ServerBox Box(IServer server)
        {
            var gameObject = new GameObject(server.Name);
            ServerBox box = gameObject.AddComponent<ServerBox>();
            gameObject.transform.SetParent(transform, false);
            box.transform.SetAsFirstSibling();
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout.GetComponent<RectTransform>());
            boxes[server] = box;
            return box;
        }

        void IServerCreationObserver.NotifyRemoteCreated(Remote remote)
        {
            var box = Box(remote);
            remote.ObserveInstallations(box);
        }
    }
}