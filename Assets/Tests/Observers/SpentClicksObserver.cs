using model;

namespace tests.observers
{
    class SpentClicksObserver : IClickObserver
    {
        public int Spent { get; private set; }

        void IClickObserver.NotifyClicks(int spent, int remaining)
        {
            Spent = spent;
        }
    }
}
