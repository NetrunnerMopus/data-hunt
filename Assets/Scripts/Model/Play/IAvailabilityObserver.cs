namespace model.play
{
    public interface IAvailabilityObserver<T>
    {
        void Notify(bool available, T resource);
    }
}