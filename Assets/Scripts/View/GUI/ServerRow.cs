using System;
using System.Collections.Generic;
using model.zones.corp;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class ServerRow
    {
        private GameObject gameObject;
        private HorizontalLayoutGroup layout;
        private readonly Dictionary<IServer, ServerBox> boxesPerServer = new Dictionary<IServer, ServerBox>();
        public event Action<ServerBox> BoxAdded = delegate { };
        public event Action<ServerBox> BoxRemoved = delegate { };
        private BoardParts parts;

        public ServerRow(GameObject gameObject, BoardParts parts, Zones zones)
        {
            this.gameObject = gameObject;
            this.parts = parts;
            layout = gameObject.AddComponent<HorizontalLayoutGroup>();
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;
            layout.childAlignment = TextAnchor.UpperLeft;
            layout.spacing = 4;
            zones.RemoteAdded += RenderNewRemote;
            zones.RemoteRemoved += DestroyDisappearingRemote;
        }

        public ServerBox Box(IServer server)
        {
            var serverObject = new GameObject(server.Zone.Name);
            var box = new ServerBox(serverObject, server, parts);
            serverObject.transform.SetParent(gameObject.transform, false);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout.GetComponent<RectTransform>());
            boxesPerServer[server] = box;
            return box;
        }

        private void RenderNewRemote(Remote remote)
        {
            var box = Box(remote);
            box.ShowCards();
            BoxAdded(box);
        }

        private void DestroyDisappearingRemote(Remote remote)
        {
            var box = boxesPerServer[remote];
            BoxRemoved(box);
            GameObject.Destroy(box.gameObject);
        }

        public IEnumerable<ServerBox> ListBoxes() => boxesPerServer.Values;
    }
}
