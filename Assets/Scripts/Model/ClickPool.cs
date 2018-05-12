using System;
using System.Collections.Generic;

namespace model
{
    public class ClickPool
    {
        private int capacity = 0;
        private int spent = 0;
        private HashSet<IClickObserver> observers = new HashSet<IClickObserver>();

        public void Replenish()
        {
            spent = 0;
            Update();
        }

        public void Spend(int clicks)
        {
            if (Unspent() >= clicks)
            {
                spent += clicks;
                Update();
            }
            else
            {
                throw new Exception("Cannot spend a click, because all of them are spent");
            }
        }

        public void Gain()
        {
            capacity += 1;
            Update();
        }

        public void Lose()
        {
            if (capacity > 0)
            {
                capacity -= 1;
                Update();
            }
        }

        private void Update()
        {
            foreach (var observer in observers)
            {
                observer.NotifyClicks(spent, Unspent());
            }
        }

        private int Unspent() {
            return capacity - spent;
        }

        public void Observe(IClickObserver observer)
        {
            observers.Add(observer);
            observer.NotifyClicks(spent, Unspent());
        }
    }

    public interface IClickObserver
    {
        void NotifyClicks(int spent, int unspent);
    }
}