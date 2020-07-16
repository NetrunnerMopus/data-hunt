using controller;
using model.timing;

namespace view.gui
{
    internal class PaidWindowView : IPaidWindowObserver
    {
        private PaidWindow window;
        private PaidWindowPass pass;
        private DropZone paidChoice;

        public PaidWindowView(PaidWindow window, PaidWindowPass pass, DropZone paidChoice)
        {
            this.window = window;
            this.pass = pass;
            this.paidChoice = paidChoice;
            pass.Represent(window, paidChoice);
            SetActive(false);
            window.ObserveWindow(this);
        }

        private void SetActive(bool active)
        {
            pass.gameObject.SetActive(active);
            paidChoice.gameObject.SetActive(active);
        }

        void IPaidWindowObserver.NotifyPaidWindowClosed(PaidWindow window)
        {
            SetActive(false);
        }

        void IPaidWindowObserver.NotifyPaidWindowOpened(PaidWindow window)
        {
            SetActive(true);
        }
    }
}
