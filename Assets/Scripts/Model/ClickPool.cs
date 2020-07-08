using System;
using System.Collections.Generic;

namespace model
{
    public class ClickPool
    {
        public int NextReplenishment { get; private set; }
        private int allotted = 0;
        public int Spent { get; private set; } = 0;
        public int Remaining { get { return allotted - Spent; } }
        private HashSet<IClickObserver> observers = new HashSet<IClickObserver>();
        public event EventHandler<ClickPool> Changed = delegate { };

        public ClickPool(int defaultReplenishment)
        {
            NextReplenishment = defaultReplenishment;
        }

        public void Spend(int clicks)
        {
            if (Remaining >= clicks)
            {
                Spent += clicks;
                Update();
            }
            else
            {
                throw new Exception("Cannot spend a click, because all of them are spent");
            }
        }

        public void Replenish()
        {
            Gain(NextReplenishment);
        }

        private void Gain(int clicks)
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
            Spent = 0;
            Update();
        }

        private void Update()
        {
            Changed(this, this);
            foreach (var observer in observers)
            {
                observer.NotifyClicks(Spent, Remaining);
            }
        }

        public void Observe(IClickObserver observer)
        {
            observers.Add(observer);
            observer.NotifyClicks(Spent, Remaining);
        }
    }

    public interface IClickObserver
    {
        void NotifyClicks(int spent, int remaining);
    }
}
