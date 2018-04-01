using System;
using view;

namespace model
{
    public class ClickPool
    {
        private int capacity = 0;
        private int spent = 0;
        private ClickPoolView view;

        public ClickPool(ClickPoolView view)
        {
            this.view = view;
        }

        public void Replenish()
        {
            spent = 0;
            UpdateView();
        }

        public void Spend()
        {
            if (spent < capacity)
            {
                spent += 1;
                UpdateView();
            }
            else
            {
                throw new Exception("Cannot spend a click, because all of them are spent");
            }
        }

        public void Gain()
        {
            capacity += 1;
            UpdateView();
        }

        public void Lose()
        {
            if (capacity > 0)
            {
                capacity -= 1;
                UpdateView();
            }
        }

        private void UpdateView()
        {
            view.UpdateClicks(spent, capacity - spent);
        }
    }
}