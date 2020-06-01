using controller;
using model.timing;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class PaidWindowView : MonoBehaviour, IPaidWindowObserver
    {
        private PaidWindow window;
        public DropZone Sink { get; private set; }

        public PaidWindowView Represent(PaidWindow window)
        {
            this.window = window;
            var pass = CreatePass(gameObject).AddComponent<PaidWindowPass>();
            Sink = CreateSink(gameObject).AddComponent<DropZone>();
            pass.Represent(window, Sink);
            gameObject.SetActive(false);
            window.ObserveWindow(this);
            return this;
        }

        private GameObject CreatePass(GameObject parent)
        {
            var pass = new GameObject("Pass");
            pass.AttachTo(parent);
            var image = pass.AddComponent<Image>();
            image.sprite = Resources.Load<Sprite>("Images/UI/hourglass");
            image.preserveAspect = true;
            var rectangle = image.rectTransform;
            rectangle.anchorMin = Vector2.zero;
            rectangle.anchorMax = new Vector2(0.3f, 1.0f);
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            return pass;
        }

        private GameObject CreateSink(GameObject parent)
        {
            var sink = new GameObject("Sink");
            sink.AttachTo(parent);
            var image = sink.AddComponent<Image>();
            image.sprite = Resources.Load<Sprite>("Images/UI/paid");
            image.preserveAspect = true;
            var rectangle = image.rectTransform;
            rectangle.anchorMin = new Vector2(0.7f, 0.0f);
            rectangle.anchorMax = Vector2.one;
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            return sink;
        }

        void IPaidWindowObserver.NotifyPaidWindowClosed(PaidWindow window)
        {
            gameObject.SetActive(false);
        }

        void IPaidWindowObserver.NotifyPaidWindowOpened(PaidWindow window)
        {
            gameObject.SetActive(true);
        }

        void OnDestroy()
        {
            window.UnobserveWindow(this);
        }
    }
}
