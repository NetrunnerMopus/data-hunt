namespace model
{
    public interface IClickObserver
    {
        void NotifyClicks(int spent, int unspent);
    }
}