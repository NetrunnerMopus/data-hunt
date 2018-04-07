namespace model
{
    public interface ICost
    {
        bool CanPay(Game game);
        void Pay(Game game);
    }
}