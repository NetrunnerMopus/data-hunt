using model.zones.corp;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class ServerRow : MonoBehaviour, IServerCreationObserver
    {
        private HorizontalLayoutGroup layout;
        private readonly List<ServerBox> boxes = new List<ServerBox>();
        private HashSet<IServerBoxObserver> observers = new HashSet<IServerBoxObserver>();

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
            var box = new ServerBox(gameObject, server);
            gameObject.transform.SetParent(transform, false);
            gameObject.transform.SetAsFirstSibling();
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout.GetComponent<RectTransform>());
            boxes.Add(box);
            foreach (var observer in observers)
            {
                observer.NotifyServerBoxCreated(box);
            }
            return box;
        }

        public void Observe(IServerBoxObserver observer)
        {
            observers.Add(observer);
        }

        void IServerCreationObserver.NotifyRemoteCreated(Remote remote)
        {
            var box = Box(remote);
            remote.ObserveInstallations(box);
        }
    }

    public interface IServerBoxObserver
    {
        void NotifyServerBoxCreated(ServerBox box);
    }
}