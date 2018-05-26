using System;
using System.Collections.Generic;

namespace model
{
    public class ClickPool
    {
        private int allotted = 0;
        private int spent = 0;
        private HashSet<IClickObserver> observers = new HashSet<IClickObserver>();

        public void Spend(int clicks)
        {
            if (Remaining() >= clicks)
            {
                spent += clicks;
                Update();
            }
            else
            {
                throw new Exception("Cannot spend a click, because all of them are spent");
            }
        }

        public void Gain(int clicks)
        {
            allotted += clicks;
            Update();
        }

        public void Lose(int loss)
        {
            if (allotted >= loss)
            {
                allotted -= loss;
            }
            else
            {
                allotted = 0;
            }
            Update();
        }

        public void Reset()
        {
            allotted = 0;
            spent = 0;
            Update();
        }

        private void Update()
        {
            foreach (var observer in observers)
            {
                observer.NotifyClicks(spent, Remaining());
            }
        }

        public int Remaining()
        {
            return allotted - spent;
        }

        public void Observe(IClickObserver observer)
        {
            observers.Add(observer);
            observer.NotifyClicks(spent, Remaining());
        }
    }

    public interface IClickObserver
    {
        void NotifyClicks(int spent, int remaining);
    }
}