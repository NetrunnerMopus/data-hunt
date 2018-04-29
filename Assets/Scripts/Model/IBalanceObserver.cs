namespace model
{
    public interface IBalanceObserver
    {
        void Notify(int balance);
    }
}