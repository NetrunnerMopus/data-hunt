using controller;
using model.timing;

namespace view.gui
{
    internal class PaidWindowView
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
            window.Opened += Open;
            window.Closed += Close;
        }

        private void SetActive(bool active)
        {
            pass.gameObject.SetActive(active);
            paidChoice.gameObject.SetActive(active);
        }

        private void Close(PaidWindow window)
        {
            SetActive(false);
        }

        private void Open(PaidWindow window)
        {
            SetActive(true);
            pass.transform.SetAsLastSibling();
            paidChoice.transform.SetAsLastSibling();
        }
    }
}
