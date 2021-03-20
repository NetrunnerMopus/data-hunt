using model;

namespace tests.observers
{
    class SpentClicksObserver
    {
        public int Spent { get; private set; }

        public void RememberSpent(ClickPool clicks)
        {
            Spent = clicks.Spent;
        }
    }
}
