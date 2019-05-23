using model.zones.corp;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class ServerRow : MonoBehaviour, IRemoteObserver
    {
        private HorizontalLayoutGroup layout;
        private readonly List<ServerBox> boxes = new List<ServerBox>();
        private HashSet<IServerBoxObserver> observers = new HashSet<IServerBoxObserver>();

        public void Represent(Zones zones)
        {
            zones.ObserveRemotes(this);
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
            var gameObject = new GameObject(server.Zone.Name);
            var box = new ServerBox(gameObject, server);
            gameObject.transform.SetParent(transform, false);
            gameObject.transform.SetAsFirstSibling();
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout.GetComponent<RectTransform>());
            boxes.Add(box);
            foreach (var observer in observers)
            {
                observer.NotifyServerBox(box);
            }
            return box;
        }

        public void Observe(IServerBoxObserver observer)
        {
            observers.Add(observer);
            foreach (var box in boxes)
            {
                observer.NotifyServerBox(box);
            }
        }

        void IRemoteObserver.NotifyRemoteExists(Remote remote)
        {
            var box = Box(remote);
            remote.ObserveInstallations(box);
        }
    }

    public interface IServerBoxObserver
    {
        void NotifyServerBox(ServerBox box);
    }
}